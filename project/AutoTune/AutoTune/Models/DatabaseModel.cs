﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;

namespace AutoTune.Models
{
	public abstract partial class DatabaseModel
	{
		private readonly Hashtable Values = new Hashtable();

		private static readonly string[] Protected = {"ID", "Added"};

		public object this[string columnName]
		{
			get { return Values[columnName]; }
			set
			{
				if (Protected.Contains(columnName)) throw new DatabaseException("Cannot set protected field \"" + columnName + "\".");
				else Values[columnName] = value;
			}
		}

		private void SetAdded()
		{
			if(Added == null)
				Values["Added"] = DateTime.Now;
		}

		private void SetID(int id)
		{
			if(ID == null)
				Values["ID"] = id;
		}

		public int? ID { get { return (int?)Values["ID"]; } }
		public DateTime? Added { get { return (DateTime?)Values["Added"]; } }

		public abstract bool IsDatabaseField(string fieldName);
		public abstract string TableName();

		public bool AllFieldsAreValid()
		{
			FieldInfo[] fields = GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				object value = field.GetValue(this);
				IsValid(value, field.Name, GetType());
			}

			return true;
		}

		public void Commit()
		{
			if (!AllFieldsAreValid())
				throw new DatabaseException("Cannot commit; instance contains invalid values.");

			if (ID == null)
			{
				CommitNewDatabaseRow();
			}
			else
			{
				CommitDatabaseFieldValues();
			}
		}

		/// <summary>
		/// Updates an existing record in the database.
		/// </summary>
		private void CommitDatabaseFieldValues()
		{
			OpenConnection();

			Hashtable databaseValues = GetDatabaseValues();
			foreach(string noUpdate in Protected)
				databaseValues.Remove(noUpdate);
			
			List<string> columnSetters = new List<string>();

			foreach(string key in databaseValues.Keys)
				columnSetters.Add(string.Format("{0} = {1}", key, SqlEscaped(databaseValues[key])));

			string setters = string.Join(", ", columnSetters.ToArray());

			string commandString = string.Format("UPDATE {0} SET {1} WHERE ID = {2}", TableName(), setters, ID);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			command.ExecuteNonQuery();

			CloseConnection();
		}

		/// <summary>
		/// Creates a new row in the database.
		/// </summary>
		private void CommitNewDatabaseRow()
		{
			SetAdded();

			OpenConnection();

			Hashtable databaseValues = GetDatabaseValues();

			List<string> columns = new List<string>();
			List<string> values = new List<string>();

			foreach(DictionaryEntry entry in databaseValues)
			{
				columns.Add((string)entry.Key);
				values.Add(SqlEscaped(entry.Value));
			}

			string allColumns = string.Join(", ", columns.ToArray());
			string allValues = string.Join(", ", values.ToArray());

			string commandString = string.Format(
				"INSERT INTO {0} ({1}) VALUES ({2})",
				TableName(), allColumns, allValues);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			command.ExecuteNonQuery();

			UpdateID();

			CloseConnection();
		}

		protected static Employee[] Find(string tableName, Hashtable conditions)
		{
			if (conditions == null || conditions.Count == 0)
				return FindAll(tableName);

			OpenConnection();

			List<string> clauses = new List<string>();

			foreach(DictionaryEntry condition in conditions)
			{
				clauses.Add(
					string.Format("{0} = {1}",
					              condition.Key, SqlEscaped(condition.Value)));
			}

			string whereClause = string.Join(" AND ", clauses.ToArray());

			string commandString = string.Format(
				"SELECT * FROM {0} WHERE {1}",
				tableName, whereClause);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			List<Employee> employees = new List<Employee>();

			while (reader.Read())
			{
				Employee employee = new Employee();
				employee.UpdateDatabaseFieldValues(reader);
				employees.Add(employee);
			}

			CloseConnection();

			return employees.ToArray();
		}

		protected static Employee[] FindAll(string tableName)
		{
			OpenConnection();

			string commandString = string.Format("SELECT * FROM {0}", tableName);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			List<Employee> employees = new List<Employee>();

			while (reader.Read())
			{
				Employee employee = new Employee();
				employee.UpdateDatabaseFieldValues(reader);
				employees.Add(employee);
			}

			CloseConnection();

			return employees.ToArray();
		}

		public static string GenerateNewSalt()
		{
			return Hash(String.Format("{0}{1}", DateTime.Now, (new Random()).NextDouble()));
		}

		private string[] GetColumnNames()
		{
			List<string> columnNames = new List<string>();
			foreach(string key in Values.Keys)
			{
				if (IsDatabaseField(key) || Protected.Contains(key))
					columnNames.Add(key);
			}

			columnNames.Sort();

			return columnNames.ToArray();
		}

		private Hashtable GetDatabaseValues()
		{
			Hashtable databaseValues = new Hashtable();
			foreach(string key in Values.Keys)
			{
				if (IsDatabaseField(key) || Protected.Contains(key))
					databaseValues.Add(key, Values[key]);
			}

			return databaseValues;
		}

		public static string Hash(string value)
		{
			byte[] input = Encoding.UTF8.GetBytes(value);
			SHA1CryptoServiceProvider crypt = new SHA1CryptoServiceProvider();
			byte[] output = crypt.ComputeHash(input);
			string result = "";
			foreach (byte b in output)
			{
				result += b.ToString("X2");
			}
			return result;
		}

		public static bool IsValid(object value, string fieldName, Type self)
		{
			MethodInfo method = self.GetMethod("IsValid" + fieldName);
			if (method == null)
			{
				return true;
			}
			else
			{
				bool isValid = (bool)method.Invoke(null, new[] { value });
				return isValid;
			}
		}

		public static string SqlEscaped(object value)
		{
			if (value == null)
				return "NULL";
			else if (value is string)
				return SqlEscaped((string)value);
			else if (value is DateTime)
				return SqlEscaped((DateTime) value);
			else
				return value.ToString();
		}

		public static string SqlEscaped(string value)
		{
			return string.Format("\"{0}\"", value.Replace("\n", "\\n").Replace("\"", "\\\""));
		}

		public static string SqlEscaped(DateTime value)
		{
			return string.Format("\"{0}\"", value.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		public void Update()
		{
			if (ID == null)
			{
				string message = string.Format(
					"Cannot update {0} instance; does not exist in database.",
					GetType());
				throw new DatabaseException(message);
			}
			else
			{
				UpdateDatabaseFieldValues();
			}
		}


		private void UpdateDatabaseFieldValues()
		{
			OpenConnection();

			string columns = string.Join(", ", GetColumnNames());

			string commandString = string.Format(
				"SELECT {0} FROM {1} WHERE ID = {2}",
				columns, TableName(), ID);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				UpdateDatabaseFieldValues(reader);
			}
			else
			{
				// This is an internal exception because
				// no instance should have an id without having
				// been committed or read from the database.
				throw new InternalException(string.Format(
					"{0}[{1}] instance not found in table {2}!",
					GetType(),	// The model name
					ID, TableName()));
			}

			CloseConnection();
		}

		private void UpdateDatabaseFieldValues(MySqlDataReader reader)
		{
			for (int i = 0; i < reader.FieldCount; ++i)
			{
				Values[reader.GetName(i)] = reader[i];
			}
		}

		/// <summary>
		/// Finds a row in the database with all values identical to this one
		/// and gets its ID.
		/// </summary>
		private void UpdateID()
		{
			if (ID != null) return;

			OpenConnection();

			Hashtable databaseValues = GetDatabaseValues();

			databaseValues.Remove("ID");

			List<string> columnSetters = new List<string>();

			foreach(string key in databaseValues.Keys)
			{
				if(databaseValues[key] == null)
					columnSetters.Add(string.Format("ISNULL({0})", key));
				else
					columnSetters.Add(string.Format("{0} = {1}", key, SqlEscaped(databaseValues[key])));
			}

			string conditions = string.Join(" AND ", columnSetters.ToArray());

			string commandString = string.Format(
				"SELECT ID FROM {0} WHERE {1} ORDER BY Added DESC",
				TableName(), conditions);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				SetID((int) reader[0]);
			}
			else
			{
				throw new InternalException(string.Format(
				                            	"Committed {0} not found in database.",
				                            	GetType()));	// Model name
			}

			CloseConnection();
		}
	}
}
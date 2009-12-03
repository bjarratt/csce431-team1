using System;
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
		public int? ID { get; protected set; }
		public DateTime? Added { get; protected set; }

		public abstract bool IsDatabaseField(string fieldName);
		public abstract string TableName();

		protected DatabaseModel() { ID = null; Added = null; }

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

			string[] columnNames = GetColumnNames();
			string[] columnValues = GetColumnValues();
			
			List<string> columnSetters = new List<string>();

			for(int i = 0; i < columnNames.Length; ++i)
			{
				columnSetters.Add(string.Format("{0} = {1}", columnNames[i], columnValues[1]));
			}

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
			Added = DateTime.Now;

			OpenConnection();

			string[] columnNames = GetColumnNames();
			string[] columnValues = GetColumnValues();

			string columns = string.Join(", ", columnNames);
			string values = string.Join(", ", columnValues);

			string commandString = string.Format(
				"INSERT INTO {0} ({1}) VALUES ({2})",
				TableName(), columns, values);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			command.ExecuteNonQuery();

			UpdateID();

			CloseConnection();
		}

		public static string GenerateNewSalt()
		{
			return Hash(String.Format("{0}{1}", DateTime.Now, (new Random()).NextDouble()));
		}

		private string[] GetColumnNames()
		{
			FieldInfo[] fields = GetFields();

			List<string> columnNames = new List<string>();

			foreach (FieldInfo field in fields)
			{
				if (IsDatabaseField(field.Name))
					columnNames.Add(field.Name);
			}

			columnNames.Add("Added");

			return columnNames.ToArray();
		}

		private string[] GetColumnValues()
		{
			FieldInfo[] fields = GetFields();
			List<string> columnValues = new List<string>();
			foreach (FieldInfo field in fields)
			{
				if(IsDatabaseField(field.Name))
				{
					columnValues.Add(SqlEscaped(field.GetValue(this)));
				}
			}

			columnValues.Add(SqlEscaped(Added));

			return columnValues.ToArray();
		}

		/// <summary>
		/// Gets a list of fields in the (derived) database model.
		/// </summary>
		/// <returns>
		/// An array of FieldInfo values. Always returned in the same order.
		/// </returns>
		private FieldInfo[] GetFields()
		{
			FieldInfo[] fields = GetType().GetFields();
			fields.OrderBy(field => field.Name);

			return fields;
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

			string[] columnNames = GetColumnNames();

			string columns = string.Join(", ", columnNames);

			string commandString = string.Format("SELECT {0} FROM {1}", columns, TableName());

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

		protected void UpdateDatabaseFieldValues(MySqlDataReader reader)
		{
			FieldInfo[] fields = GetFields();

			if (fields.Length != reader.FieldCount)
				throw new InternalException(string.Format(
					"Mismatch between instance and database field counts: {0}[instance] != {1}[database].",
					fields.Length,
					reader.FieldCount));

			for (int i = 0; i < fields.Length; ++i)
			{
				fields[i].SetValue(this, reader[i]);
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

			string[] columnNames = GetColumnNames();
			string[] columnValues = GetColumnValues();

			List<string> columnSetters = new List<string>();

			for (int i = 0; i < columnNames.Length; ++i)
			{
				if (columnValues[i] == "NULL")
					columnSetters.Add(string.Format("ISNULL({0})", columnNames[i]));
				else
					columnSetters.Add(string.Format("{0} = {1}", columnNames[i], columnValues[i]));
			}

			string setters = string.Join(" AND ", columnSetters.ToArray());

			string commandString = string.Format(
				"SELECT ID FROM {0} WHERE {1} ORDER BY Added DESC",
				TableName(), setters);

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				ID = (int)reader[0];
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
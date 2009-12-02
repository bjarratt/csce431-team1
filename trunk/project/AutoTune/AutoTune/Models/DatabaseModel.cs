using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AutoTune.Models
{
	public abstract class DatabaseModel
	{
		public int? ID { get; protected set; }

		public class DatabaseException : Exception
		{
			public DatabaseException(string message) :
				base(message) { }
		}
		private class InternalException : Exception
		{
			public InternalException(string message) :
				base(message) { }
		}

		public abstract bool IsDatabaseField(string field_name);
		public abstract string TableName();

		private MySqlConnection Connection = null;
		private static string ConnectionString = "datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize";
		private void OpenConnection()
		{
			Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
		}

		private void CloseConnection()
		{
			Connection.Close();
		}

		public static string GenerateNewSalt()
		{
			return Hash(String.Format("{0}{1}", DateTime.Now, (new Random()).NextDouble()));
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

		public static bool IsValid(object value, string field_name, Type self)
		{
			FieldInfo field = self.GetField(field_name);

			MethodInfo method = self.GetMethod("IsValid" + field_name);
			if (method == null)
			{
				return true;
			}
			else
			{
				bool is_valid = (bool)method.Invoke(null, new object[] { value });
				return is_valid;
			}
		}

		public bool AllFieldsAreValid()
		{
			FieldInfo[] fields = this.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				object value = field.GetValue(this);
				IsValid(value, field.Name, this.GetType());
			}

			return true;
		}

		public static string SqlEscaped(object value)
		{
			if (value is string)
				return SqlEscaped((string)value);
			else
				return value.ToString();
		}

		public static string SqlEscaped(string value)
		{
			return string.Format("\"{0}\"", value.Replace("\"", "\\\""));
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

		public void Update()
		{
			if (ID == null)
			{
				string message = string.Format(
					"Cannot update {0} instance; does not exist in database.",
					this.GetType().ToString());
				throw new DatabaseException(message);
			}
			else
			{
				UpdateDatabaseFieldValues();
			}
		}

		private string[] GetColumnNames()
		{
			FieldInfo[] fields = GetFields();

			List<string> column_names = new List<string>();

			foreach (FieldInfo field in fields)
			{
				if (IsDatabaseField(field.Name))
					column_names.Add(field.Name);
			}

			return column_names.ToArray();
		}

		private string[] GetColumnValues()
		{
			FieldInfo[] fields = GetFields();
			List<string> column_values = new List<string>();
			foreach (FieldInfo field in fields)
			{
				if(IsDatabaseField(field.Name))
				{
					column_values.Add(SqlEscaped(field.GetValue(this)));
				}
			}

			return column_values.ToArray();
		}


		/// <summary>
		/// Gets a list of fields in the (derived) database model.
		/// </summary>
		/// <returns>
		/// An array of FieldInfo values. Always returned in the same order.
		/// </returns>
		private FieldInfo[] GetFields()
		{
			FieldInfo[] fields = this.GetType().GetFields();
			fields.OrderBy(field => field.Name);

			return fields;
		}

		/// <summary>
		/// Updates an existing record in the database.
		/// </summary>
		private void CommitDatabaseFieldValues()
		{
			OpenConnection();

			string[] column_names = GetColumnNames();
			string[] column_values = GetColumnValues();
			
			List<string> column_setters = new List<string>();

			for(int i = 0; i < column_names.Length; ++i)
			{
				column_setters.Add(string.Format("{0} = {1}", column_names[i], column_values[1]));
			}

			string setters = string.Join(", ", column_setters.ToArray());

			string command_string = string.Format("UPDATE {0} SET {1} WHERE ID = {2}", TableName(), setters, ID);

			MySqlCommand command = new MySqlCommand(command_string, Connection);
			command.ExecuteNonQuery();

			CloseConnection();
		}

		/// <summary>
		/// Creates a new row in the database.
		/// </summary>
		private void CommitNewDatabaseRow()
		{
			OpenConnection();

			string[] column_names = GetColumnNames();
			string[] column_values = GetColumnValues();

			string columns = string.Join(", ", column_names);
			string values = string.Join(",", column_values);

			string command_string = string.Format(
				"INSERT INTO {0} ({1}) VALUES ({2})",
				TableName(), columns, values);

			MySqlCommand command = new MySqlCommand(command_string, Connection);
			command.ExecuteNonQuery();

			UpdateID();

			CloseConnection();
		}

		/// <summary>
		/// Finds a row in the database with all values identical to this one
		/// and gets its ID.
		/// </summary>
		private void UpdateID()
		{
			if (ID != null) return;

			OpenConnection();

			string[] column_names = GetColumnNames();
			string[] column_values = GetColumnValues();

			List<string> column_setters = new List<string>();

			for (int i = 0; i < column_names.Length; ++i)
			{
				column_setters.Add(string.Format("{0} = {1}", column_names[i], column_values[1]));
			}

			string setters = string.Join(", ", column_setters.ToArray());

			string command_string = string.Format(
				"SELECT ID FROM {0} WHERE {1}",
				TableName(), setters);

			MySqlCommand command = new MySqlCommand(command_string, Connection);
			command.ExecuteReader(
			command.ExecuteNonQuery();

			CloseConnection();
		}

		private void UpdateDatabaseFieldValues(MySqlDataReader reader)
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

		private void UpdateDatabaseFieldValues()
		{
			OpenConnection();

			string[] column_names = GetColumnNames();

			string columns = string.Join(", ", column_names);

			string command_string = string.Format("SELECT {0} FROM {1}", columns, TableName());

			MySqlCommand command = new MySqlCommand(command_string, Connection);
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
					this.GetType().ToString(),	// The model name
					ID, TableName()));
			}

			CloseConnection();
		}
	}
}
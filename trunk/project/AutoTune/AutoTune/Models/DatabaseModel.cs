using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

		/// <summary>
		/// This is overridden in subclasses to determine which fields
		/// are to be stored in the database, and which are utility
		/// fields only used within the C# instances. For example,
		/// the Employee class has a Password field that is not saved;
		/// only the PasswordHash is saved, so IsDatabaseField("Password")
		/// will return false.
		/// </summary>
		/// <param name="fieldName">The column name to be checked.</param>
		/// <returns>True if the field needs to be saved to the database.</returns>
		public abstract bool IsDatabaseField(string fieldName);

		/// <summary>
		/// Overridden by subclasses to provide the name of the MySQL table
		/// corresponding to that class.
		/// </summary>
		/// <returns>The table name as a string.</returns>
		public abstract string TableName();

		/// <summary>
		/// Runs any field validators found in a subclass.
		/// Finds field validators through introspection.
		/// </summary>
		/// <returns>False if any field validator returns false.</returns>
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

		/// <summary>
		/// Sets the foreign key of this row to the id of the desired parent.
		/// </summary>
		/// <param name="parent">The parent row to be attached to.</param>
		/// <param name="foreignKey">The column name of the foreign key.</param>
		protected void AttachTo(DatabaseModel parent, string foreignKey)
		{
			if(!IsDatabaseField(foreignKey))
				throw new InternalException(string.Format("Cannot attach; field {0} is not a database field.", foreignKey));

			this[foreignKey] = parent.ID;
			Commit();
		}

		/// <summary>
		/// Creates a new row in the specified join table, joining this
		/// row to the desired parent.
		/// </summary>
		/// <param name="parent">The parent row to be attached to.</param>
		/// <param name="joinModelConstructor">A constructor for the join model.</param>
		/// <param name="parentKey">The foreign key in the join table corresponding to the parent row.</param>
		/// <param name="childKey">The foreign key in the join table corresponding to the child row.</param>
		protected void AttachTo(DatabaseModel parent, Constructor joinModelConstructor, string parentKey, string childKey)
		{
			DatabaseModel joinModel = joinModelConstructor();
			joinModel[parentKey] = parent.ID;
			joinModel[childKey] = ID;
			joinModel.Commit();
		}

		/// <summary>
		/// Creates a new row in the specified join table, joining this
		/// row to the desired parent.
		/// </summary>
		/// <param name="parent">The parent row to be attached to.</param>
		/// <param name="joinModelConstructor">A constructor for the join model.</param>
		/// <param name="parentKey">The foreign key in the join table corresponding to the parent row.</param>
		/// <param name="childKey">The foreign key in the join table corresponding to the child row.</param>
		/// <param name="payload">Any additional values to be set in the new join table row.</param>
		protected void AttachTo(DatabaseModel parent, Constructor joinModelConstructor, string parentKey, string childKey, Hashtable payload)
		{
			DatabaseModel joinModel = joinModelConstructor();
			joinModel[parentKey] = parent.ID;
			joinModel[childKey] = ID;

			foreach(DictionaryEntry entry in payload)
				joinModel[(string)entry.Key] = entry.Value;

			joinModel.Commit();
		}

		/// <summary>
		/// Saves any changes to this object into the database.
		/// If the instance is new (therefore, does not have a valid
		/// ID), a new row is created.
		/// </summary>
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

		protected delegate DatabaseModel Constructor();

		/// <summary>
		/// Get all rows meeting the specified conditions.
		/// </summary>
		/// <param name="tableName">The name of the table being fetched from.</param>
		/// <param name="modelConstructor">A constructor for the result objects.</param>
		/// <param name="conditions">ColumnName : Value pairs for the conditions in the WHERE clause.</param>
		/// <returns>A list of matching database models.</returns>
		protected static DatabaseModel[] Find(string tableName, Constructor modelConstructor, Hashtable conditions)
		{
			if (conditions == null || conditions.Count == 0)
				return FindAll(tableName, modelConstructor);

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

			return ManualFind(commandString, modelConstructor);
		}

		/// <summary>
		/// Gets all rows in a specified table.
		/// </summary>
		/// <param name="tableName">The MySQL table name.</param>
		/// <param name="modelConstructor">A constructor for the result objects.</param>
		/// <returns>List of all objects in the specified table.</returns>
		private static DatabaseModel[] FindAll(string tableName, Constructor modelConstructor)
		{
			OpenConnection();

			string commandString = string.Format("SELECT * FROM {0}", tableName);

			return ManualFind(commandString, modelConstructor);
		}

		/// <summary>
		/// Gets all the children of this object through the specified foreign key.
		/// </summary>
		/// <param name="childTableName">The name of the MySQL child table.</param>
		/// <param name="childModelConstructor">A constructor for the child models.</param>
		/// <param name="foreignKey">Column name of the foreign key.</param>
		/// <returns>List of children.</returns>
		protected DatabaseModel[] FindChildren(string childTableName, Constructor childModelConstructor, string foreignKey)
		{
			string commandString = string.Format(
				"SELECT * FROM {0} WHERE {1} = {2}",
				childTableName, foreignKey, ID);
			return ManualFind(commandString, childModelConstructor);
		}

		/// <summary>
		/// Gets all the children of this object through the specified join table.
		/// </summary>
		/// <param name="childTableName">The name of the MySQL child table.</param>
		/// <param name="childModelConstructor">A constructor for the child models.</param>
		/// <param name="joinTableName">The name of the MySQL join table.</param>
		/// <param name="parentKey">The foreign key of the join table corresponding to the parent.</param>
		/// <param name="childKey">The foreign key of the join table corresponding to the child.</param>
		/// <returns>List of children..</returns>
		protected DatabaseModel[] FindChildren(string childTableName, Constructor childModelConstructor, string joinTableName, string parentKey, string childKey)
		{
			string commandString = string.Format(
				"SELECT {0}.* FROM {1} LEFT JOIN {0} ON {0}.ID = {1}.{2} WHERE {1}.{3} = {4}",
				childTableName, joinTableName, childKey, parentKey, ID);
			return ManualFind(commandString, childModelConstructor);
		}

		/// <summary>
		/// Gets all the children of this object through the specified join table.
		/// </summary>
		/// <param name="childTableName">The name of the MySQL child table.</param>
		/// <param name="childModelConstructor">A constructor for the child models.</param>
		/// <param name="joinTableName">The name of the MySQL join table.</param>
		/// <param name="parentKey">The foreign key of the join table corresponding to the parent.</param>
		/// <param name="childKey">The foreign key of the join table corresponding to the child.</param>
		/// <param name="childConditions">Additional conditions for the rows in the child table.</param>
		/// <param name="joinConditions">Additional conditions for the rows in the join table.</param>
		/// <returns>List of children..</returns>
		protected DatabaseModel[] FindChildren(string childTableName, Constructor childModelConstructor, string joinTableName, string parentKey, string childKey, Hashtable joinConditions, Hashtable childConditions)
		{
			List<string> conditions = new List<string>();

			foreach(DictionaryEntry entry in joinConditions)
				conditions.Add(string.Format("{0}.{1} = {2}", joinTableName, entry.Key, SqlEscaped(entry.Value)));

			foreach (DictionaryEntry entry in childConditions)
				conditions.Add(string.Format("{0}.{1} = {2}", childTableName, entry.Key, SqlEscaped(entry.Value)));

			conditions.Add(string.Format("{0}.{1} = {2}", joinTableName, parentKey, ID));

			string conditionString = string.Join(" AND ", conditions.ToArray());

			string commandString = string.Format(
				"SELECT {0}.* FROM {1} LEFT JOIN {0} ON {0}.ID = {1}.{2} WHERE {3}",
				childTableName, joinTableName, childKey, conditionString);
			return ManualFind(commandString, childModelConstructor);
		}

		/// <summary>
		/// Creates a randomly-generated salt value for password hashing.
		/// </summary>
		/// <returns>The salt as a string</returns>
		public static string GenerateNewSalt()
		{
			return Hash(String.Format("{0}{1}", DateTime.Now, (new Random()).NextDouble()));
		}

		/// <summary>
		/// Gets the column names of all fields that should be
		/// saved to the database (see IsDatabaseField(...)).
		/// </summary>
		/// <returns>Column names as a list of strings.</returns>
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

		/// <summary>
		/// Gets all of the values that should be saved in
		/// the database (see IsDatabaseField(...)).
		/// </summary>
		/// <returns>Hashtable of ColumnName:Value pairs.</returns>
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

		/// <summary>
		/// Computes a printable hash of the provided value.
		/// </summary>
		/// <param name="value">The string to be hashed</param>
		/// <returns>The hashed string.</returns>
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

		/// <summary>
		/// Runs any validators defined in a subclass. Validators are
		/// expected to be named 'IsValid{FieldName}', eg 'IsValidUsername',
		/// take the value as their one parameter, and return a bool.
		/// </summary>
		/// <param name="value">The value to be checked.</param>
		/// <param name="fieldName">Field name of the value.</param>
		/// <param name="self">The type of the calling object.</param>
		/// <returns>False if any validator fails.</returns>
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

		/// <summary>
		/// Gets all values returned by an SQL select command.
		/// WARNING: DANGEROUS METHOD! USE ONLY WITH GREAT CAUTION!
		/// </summary>
		/// <param name="commandString">The SQL SELECT command.</param>
		/// <param name="modelConstructor">A constructor for the database models.</param>
		/// <returns>List of database objects.</returns>
		private static DatabaseModel[] ManualFind(string commandString, Constructor modelConstructor)
		{
			OpenConnection();

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			List<DatabaseModel> models = new List<DatabaseModel>();

			while (reader.Read())
			{
				DatabaseModel model = modelConstructor();
				model.UpdateDatabaseFieldValues(reader);
				models.Add(model);
			}

			CloseConnection();

			return models.ToArray();
		}

		/// <summary>
		/// Gets the value as a string, escaped as necessary
		/// based on its type.
		/// </summary>
		/// <param name="value">The value to be escaped.</param>
		/// <returns>The value as an escaped string.</returns>
		public static string SqlEscaped(object value)
		{
			if (value == null)
				return "NULL";
			else if (value is string)
				return SqlEscaped((string)value);
			else if (value is DateTime)
				return SqlEscaped((DateTime) value);
			else if (value.GetType() == typeof(DBNull))
				return "NULL";
			else
				return value.ToString();
		}

		/// <summary>
		/// Escapes newlines and quotation marks, and surrounds with quotation marks.
		/// </summary>
		/// <param name="value">The string to be escaped.</param>
		/// <returns>The escaped string.</returns>
		public static string SqlEscaped(string value)
		{
			return string.Format("\"{0}\"", value.Replace("\n", "\\n").Replace("\"", "\\\""));
		}

		/// <summary>
		/// Converts a DateTime object into its
		/// string representation in a format
		/// understandable by MySQL. Also surrounds
		/// with quotation marks.
		/// </summary>
		/// <param name="value">The DateTime to be escaped.</param>
		/// <returns>The DateTime as a MySQL-friendly string.</returns>
		public static string SqlEscaped(DateTime value)
		{
			return string.Format("\"{0}\"", value.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		/// <summary>
		/// Restores/resets all database-backed fields of this object
		/// with values from its row in the database.
		/// REQUIRES A VALID ID!
		/// Attempting to Update() a new instance (with no ID)
		/// will result in an error.
		/// </summary>
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

		/// <summary>
		/// Restores/resets all database-backed fields of this object.
		/// See Update().
		/// </summary>
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

		/// <summary>
		/// Restores/resets all database-backed fields of this
		/// object from the provided IDataRecord. The IDataRecord is
		/// expected to contain the required data; e.g. when using
		/// MySqlDataReader, the Read() function must have already
		/// been called.
		/// </summary>
		/// <param name="reader">The values read from the database.</param>
		private void UpdateDatabaseFieldValues(IDataRecord reader)
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
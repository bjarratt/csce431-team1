using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace AutoTune.Models
{
	public class Employee : DatabaseModel
	{
		public static string[] DatabaseFields = {"Username", "PasswordHash", "Salt", "TemporaryHash"};
		public override bool IsDatabaseField(string fieldName)
		{
			return DatabaseFields.Contains(fieldName);
		}

		public override string TableName()
		{
			return "Employees";
		}

		public string Username;
		public string PasswordHash;
		public string Salt;
		public string TemporaryHash;

		public Employee()
		{ Salt = GenerateNewSalt(); }

		public static Employee Authenticate(string username, string password)
		{
			throw new NotImplementedException();
		}

		public static Regex UsernameRegex = new Regex("^\\w{6,32}$");
		public static bool IsValidUsername(string username)
		{
			return UsernameRegex.Match(username).Success;
		}

		public static Regex PasswordRegex = new Regex("^[^\\s]{4,32}$");
		public static bool IsValidPassword(string password)
		{
			return PasswordRegex.Match(password).Success;
		}

		public static Employee[] FindAll(int id)
		{
			OpenConnection();

			const string commandString = "SELECT * FROM Employees";

			MySqlCommand command = new MySqlCommand(commandString, Connection);
			MySqlDataReader reader = command.ExecuteReader();

			List<Employee> employees = new List<Employee>();

			while(reader.Read())
			{
				Employee employee = new Employee();
				employee.UpdateDatabaseFieldValues(reader);
				employees.Add(employee);
			}

			CloseConnection();

			return employees.ToArray();
		}

		public void SetPassword(string password)
		{
			if(IsValidPassword(password))
			{
				PasswordHash = Hash(password + Salt);
			}
			else
			{
				throw new DatabaseException("Invalid password.");
			}
		}
	}
}

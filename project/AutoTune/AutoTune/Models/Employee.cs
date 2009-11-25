using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace AutoTune.Models
{
	public class Employee : DatabaseModel
	{
		public enum Type{All, Manager, Salesperson, Sysadmin};

		public int? EmployeeID { get; private set; }
		public string Username { get; private set; }
		public string PasswordHash { get; private set; }
		public string Salt { get; private set; }
		public string TemporaryPassword { get; private set; }

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

		public Employee(string username, string password)
		{
			EmployeeID = null;
			if (!IsValidUsername(username))
				throw new Exception("Invalid username; cannot create Employee.");
			Username = username;
			if (Employee.Find(username) != null)
				throw new Exception("An employee with this username already exists.");
			if (!IsValidPassword(password))
				throw new Exception("Invalid password; cannot create Employee.");
			Salt = GenerateNewSalt();
			PasswordHash = Employee.Hash(password + Salt);
			TemporaryPassword = null;
		}

		public void Commit()
		{
			if (EmployeeID == null)
				CommitNewEmployee();
			else
				UpdateEmployee();
		}

		private void CommitNewEmployee()
		{
			if(!AllFieldsAreValid())
				throw new Exception("Cannot commit Employee; contains invalid data.");

			string command_string = String.Format("INSERT INTO Employees VALUES (NULL, \"{0}\", \"{1}\", \"{2}\", \"{3}\")", Username, PasswordHash, Salt, TemporaryPassword);
			MySqlConnection connection = new MySqlConnection("datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize");
			connection.Open();
			MySqlCommand command = new MySqlCommand(command_string, connection);
			command.ExecuteNonQuery();
			connection.Close();
		}

		private void UpdateEmployee()
		{
			if (!AllFieldsAreValid())
				throw new Exception("Cannot commit Employee; contains invalid data.");

			string command_string;
			if(TemporaryPassword == null)
			{
				command_string = String.Format("UPDATE Employees SET Username = {0}, PasswordHash = {1}, Salt = {2}, TemporaryPassword = NULL", Username, PasswordHash, Salt);
			}
			else
			{
				command_string = String.Format("UPDATE Employees SET Username = {0}, PasswordHash = {1}, Salt = {2}, TemporaryPassword = {3}", Username, PasswordHash, Salt, TemporaryPassword);
			}
			
			MySqlConnection connection = new MySqlConnection("datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize");
			MySqlCommand command = new MySqlCommand(command_string, connection);
			command.ExecuteNonQuery();
		}

		public static Employee[] Find(RoleType type)
		{
			throw new NotImplementedException();
		}

		private static Employee Find(string username)
		{
			if (!IsValidUsername(username))
				return null;

			string command_string = String.Format("SELECT * FROM Employees WHERE Username = \"{0}\"", username);
			MySqlConnection connection = new MySqlConnection("datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize");
			connection.Open();
			MySqlCommand command = new MySqlCommand(command_string, connection);
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				connection.Close();
				throw new NotImplementedException();
			}
			else
			{
				connection.Close();
				return null;
			}
		}

		public static Employee Authenticate(string username, string password)
		{
			Employee employee = Employee.Find(username);
			if (employee != null && employee.PasswordHash == Employee.Hash(password + employee.Salt))
				return employee;
			else
				return null;
		}

		private static Regex UsernameRegex = new Regex("^\\w{6,32}$");
		public static bool IsValidUsername(string username)
		{
			return UsernameRegex.Match(username).Success;
		}

		private static Regex PasswordRegex = new Regex("^[^\\s]{6,32}$");
		public static bool IsValidPassword(string password)
		{
			return PasswordRegex.Match(password).Success;
		}

		public static string GenerateNewSalt()
		{
			return "THIS IS A RANDOMLY GENERATED SALT--really!";
		}

		public bool AllFieldsAreValid()
		{
			if (!IsValidUsername(Username))
				return false;


			return true;
		}
	}
}

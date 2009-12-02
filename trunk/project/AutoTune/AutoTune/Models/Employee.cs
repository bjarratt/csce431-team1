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
using System.Text;
using System.Xml;

using MySql;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace AutoTune.Models
{
	public class Employee : DatabaseModel
	{
		public static string[] DatabaseFields = {"Username", "PasswordHash", "Salt", "TemporaryPassword"};
		public override bool IsDatabaseField(string field_name)
		{
			return DatabaseFields.Contains(field_name);
		}

		public int? EmployeeID;
		public string Username;
		public string PasswordHash;
		public string Salt;
		public string TemporaryPassword;

		public Employee(string username, string password)
		{
			EmployeeID = null;
			Username = username;
			if (Employee.Find(username) != null)
				throw new Exception("An employee with this username already exists.");
			Salt = GenerateNewSalt();
			PasswordHash = Employee.Hash(password + Salt);
			TemporaryPassword = null;
		}

		public Employee(MySqlDataReader reader)
		{
			EmployeeID = (int)reader[0];
			Username = (string)reader[1];
			PasswordHash = (string)reader[2];
			Salt = (string)reader[3];
			TemporaryPassword = null;
		}

		private static Employee Find(string username)
		{
			/*if (!IsValid("Username", username))
				return null;*/

			string command_string = String.Format("SELECT * FROM Employees WHERE Username = \"{0}\"", username);
			MySqlConnection connection = new MySqlConnection("datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize");
			connection.Open();
			MySqlCommand command = new MySqlCommand(command_string, connection);
			MySqlDataReader reader = command.ExecuteReader();

			if (reader.Read())
			{
				string result = String.Format("{0} : {1}", reader[0], reader[2]);
				object test = reader[1];
				Employee employee = new Employee(reader);
				connection.Close();
				return employee;
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

		public static Regex UsernameRegex = new Regex("^\\w{6,32}$");
		public static Regex PasswordRegex = new Regex("^[^\\s]{6,32}$");
	}
}

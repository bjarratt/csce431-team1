using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

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
			Employee employee = FindByUsername(username);
			if (employee == null)
				throw new DatabaseException(string.Format("User '{0}' does not exist", username));

			string hash = Hash(password + employee.Salt);
			if (hash != employee.PasswordHash)
				throw new DatabaseException("Invalid username or password");

			return employee;
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

		public static Employee[] Find(Hashtable conditions)
		{
			return Find("Employees", conditions);
		}

		public static Employee[] FindAll()
		{
			return FindAll("Employees");
		}

		public static Employee FindByUsername(string username)
		{
			Employee[] employees = Find(new Hashtable { { "Username", username } });
			if (employees.Length < 1)
				return null;
			else
				return employees[0];
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

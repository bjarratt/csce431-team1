using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoTune.Models
{
	public class Employee : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"Username", "PasswordHash", "Salt", "TemporaryHash", "DealershipID", "IsManager"};
		public override bool IsDatabaseField(string fieldName)
		{
			return DatabaseFields.Contains(fieldName);
		}

		public void SellVehicle(Vehicle vehicle, Employee salesperson, string customerInfo, double price)
		{
			if(!IsManager)
				throw new DatabaseException("Only managers can mark vehicles as sold.");

			VehicleSale sale = new VehicleSale();
			sale.CustomerInfo = customerInfo;
			sale.Price = price;
			sale.Manager = this;
			sale.Salesperson = salesperson;
			sale.Commit();

			vehicle.Sale = sale;
			vehicle.Commit();
		}

		public override string TableName()
		{
			return "Employees";
		}

		public bool IsManager
		{
			get { return (bool)this["IsManager"]; }
			set { this["IsManager"] = value; }
		}

		public string FullName
		{
			get { return (string) this["FullName"]; }
			set { this["FullName"] = value; }
		}

		public string Username
		{
			get { return (string) this["Username"]; }
			set { this["Username"] = value; }
		}

		public string Password
		{
			set{ SetPassword(value);}
		}

		public string PasswordHash
		{
			get { return (string)this["PasswordHash"]; }
		}

		public string Salt
		{
			get { return (string)this["Salt"]; }
			set { this["Salt"] = value; }
		}
		public string TemporaryHash
		{
			get { return (string)this["TemporaryHash"]; }
			set { this["TemporaryHash"] = value; }
		}

		public Dealership Location
		{
			get
			{
				if(this["DealershipID"].GetType() == typeof(DBNull))
					return null;
				else
					return Dealership.Find((int?)this["DealershipID"]);
			}
			set { this["DealershipID"] = value.ID; }
		}

		public string Email
		{
			get { return (string) this["Email"]; }
			set { this["Email"] = value; }
		}

		public string Phone
		{
			get { return (string)this["Phone"]; }
			set { this["Phone"] = value; }
		}

		public string ImageUri
		{
			get { return (string)this["ImageUri"]; }
			set { this["ImageUri"] = value; }
		}

		public Employee()
		{
			Salt = GenerateNewSalt();
			IsManager = false;
		}

		public static Employee Authenticate(string username, string password)
		{
			Employee employee = FindByUsername(username);
			if (employee == null)
				return null;

			string hash = Hash(password + employee.Salt);
			if (hash != employee.PasswordHash)
				return null;

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

		public static bool IsValidFullName(string fullName)
		{ return fullName != null; }

		public static bool IsValidEmail(string email)
		{ return email != null; }

		public static bool IsValidPhone(string phone)
		{ return phone != null; }

		public static Employee Find(int id)
		{
			return (Employee)Find("Employees", () => new Employee(), new Hashtable {{"ID", id}}).First();
		}

		public static Employee[] Find(Hashtable conditions)
		{
			return Find("Employees", () => new Employee(), conditions).Cast<Employee>().ToArray();
		}

		public static Employee[] FindAll()
		{
			return Find(null);
		}

		public static IEnumerable<Employee> FindAllNonAdmin()
		{
			IEnumerable<Employee> allEmployees = FindAll();
			List<Employee> employees = new List<Employee>();
			foreach(Employee employee in allEmployees)
				if(employee.Location != null)
					employees.Add(employee);

			return employees;
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
				this["PasswordHash"] = Hash(password + Salt);
			}
			else
			{
				throw new DatabaseException("Invalid password.");
			}
		}

		public IEnumerable<Message> GetMessages()
		{
			IEnumerable<Message> allMessages =  FindChildren("Messages", () => new Message(), "MessageRecipients", "EmployeeID", "MessageID").Cast<Message>();
			List<Message> messages = new List<Message>();
			foreach(Message message in allMessages)
				if(!message.Deleted)
					messages.Add(message);

			return messages;
		}

		public IEnumerable<Message> GetSentMessages()
		{
			return FindChildren("Messages", () => new Message(), "Sender").Cast<Message>();
		}

		public bool IsAdmin()
		{
			return this["DealershipID"].GetType() == typeof(DBNull);
		}

		public IEnumerable<VehicleSale> GetVehicleSales()
		{
			return FindChildren("VehicleSale", () => new VehicleSale(), "SalespersonID").Cast<VehicleSale>();
		}

		public IEnumerable<Dealership> GetLocations()
		{
			return FindChildren("Dealerships", () => new Dealership(), "Roles", "EmployeeID", "DealershipID").Cast<Dealership>();
		}

		public IEnumerable<Vehicle> GetVehicles()
		{
			return Location.GetVehicles();
		}
	}
}

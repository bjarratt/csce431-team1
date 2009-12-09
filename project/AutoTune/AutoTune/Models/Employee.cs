﻿using System;
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

		public override string TableName()
		{
			return "Employees";
		}

		public bool IsManager
		{
			get { return (bool)this["IsManager"]; }
			set { this["IsManager"] = value; }
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
			get { return Dealership.Find((int?)this["DealershipID"]); }
			set { this["DealershipID"] = value.ID; }
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

		public IEnumerable<Role> GetRoles()
		{
			return FindChildren("Roles", () => new Role(), "EmployeeID").Cast<Role>();
		}

		public bool IsAdmin()
		{
			return this["DealershipID"].GetType() == typeof(DBNull);
		}

		public IEnumerable<VehicleSale> GetVehicleSales()
		{
			return FindChildren("VehicleSale", () => new VehicleSale(), "SalespersonID").Cast<VehicleSale>();
		}

		public IEnumerable<VehicleTrade> GetInitiatedTrades()
		{
			return FindChildren("VehicleTrade", () => new VehicleTrade(), "Initiator").Cast<VehicleTrade>();
		}

		public IEnumerable<VehicleTrade> GetPendingTrades()
		{
			return FindChildren("VehicleTrade", () => new VehicleTrade(), "Target").Cast<VehicleTrade>();
		}

		public IEnumerable<Dealership> GetLocations()
		{
			return FindChildren("Dealerships", () => new Dealership(), "Roles", "EmployeeID", "DealershipID").Cast<Dealership>();
		}
	}
}

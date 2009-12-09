using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoTune.Models
{
	public class Dealership : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"Name", "Location", "Email", "Phone"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "Dealerships"; }

		public string Name
		{
			get { return (string) this["Name"]; }
			set { this["Name"] = value; }
		}

		public string Location
		{
			get { return (string)this["Location"]; }
			set { this["Location"] = value; }
		}

		public string Email
		{
			get { return (string)this["Email"]; }
			set { this["Email"] = value; }
		}

		public string Phone
		{
			get { return (string)this["Phone"]; }
			set { this["Phone"] = value; }
		}

		public static bool IsValidPhone(string phone)
		{ return phone != null; }

		public static bool IsValidEmail(string email)
		{ return email != null; }

		public static bool IsValidLocation(string location)
		{ return location != null; }

		public static IEnumerable<Dealership> Find(Hashtable conditions)
		{
			return Find("Dealerships", () => new Dealership(), conditions).Cast<Dealership>();
		}

		public IEnumerable<Employee> GetEmployees()
		{
			return
				FindChildren("Employees", () => new Employee(), "Roles", "DealershipID", "EmployeeID").Cast<Employee>();
		}

		public IEnumerable<Vehicle> GetVehicles()
		{
			IEnumerable<Vehicle> allVehicles = FindChildren("Vehicles", () => new Vehicle(), "DealershipID").Cast<Vehicle>();
			List<Vehicle> unsoldVehicles = new List<Vehicle>();
			foreach(Vehicle vehicle in allVehicles)
				if(vehicle.Sale == null)
					unsoldVehicles.Add(vehicle);

			return unsoldVehicles;
		}

		public static Dealership Find(int? id)
		{
			if (id == null)
				return null;
			else
				return (Dealership)Find("Dealerships", () => new Dealership(), new Hashtable {{"ID", id}}).First();
		}

		public static IEnumerable<Dealership> FindAll()
		{
			return Find("Dealerships", () => new Dealership(), null).Cast<Dealership>();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}

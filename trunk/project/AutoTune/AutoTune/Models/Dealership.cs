using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoTune.Models
{
	public class Dealership : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "Dealerships"; }

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
			return FindChildren("Vehicles", () => new Vehicle(), "DealershipID").Cast<Vehicle>();
		}
	}
}

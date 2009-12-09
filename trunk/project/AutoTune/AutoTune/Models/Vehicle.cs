using System.Collections.Generic;
using System.Linq;

namespace AutoTune.Models
{
	public class Vehicle : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"VehicleID", "VehicleTradeID"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "Vehicles"; }

		public static IEnumerable<Vehicle> FindAll()
		{
			return Find("Vehicles", () => new Vehicle(), null).Cast<Vehicle>();
		}
	}
}

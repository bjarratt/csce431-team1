using System.Collections;
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

		public static Dealership[] Find(Hashtable conditions)
		{
			return Find("Dealerships", () => new Dealership(), conditions).Cast<Dealership>().ToArray();
		}
	}
}

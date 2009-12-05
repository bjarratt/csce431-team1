using System.Linq;

namespace AutoTune.Models
{
	public class VehicleTradeItem : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"VehicleID", "VehicleTradeID"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "VehicleTradeItems"; }
	}
}

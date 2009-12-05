using System.Linq;

namespace AutoTune.Models
{
	public class VehicleTrade : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {
		                                                  	"Initiator", "Target", "Initiated", "Accepted", "Cancelled",
		                                                  	"Rejected"
		                                                  };
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "VehicleTrades"; }
	}
}

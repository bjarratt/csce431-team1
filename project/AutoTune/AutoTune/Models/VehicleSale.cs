using System.Linq;

namespace AutoTune.Models
{
	public class VehicleSale : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {
		                                                  	"VehicleID", "SalespersonID", "CustomerInfo", "Initiated",
		                                                  	"Finalized", "ManagerID"
		                                                  };
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "VehicleSales"; }
	}
}

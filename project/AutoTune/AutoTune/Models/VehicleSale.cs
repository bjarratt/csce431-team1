using System.Collections;
using System.Linq;

namespace AutoTune.Models
{
	public class VehicleSale : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"ManagerID", "SalespersonID", "CustomerInfo", "Price"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "VehicleSales"; }

		public static VehicleSale Find(int id)
		{
			return (VehicleSale)Find("VehicleSales", () => new VehicleSale(), new Hashtable {{"ID", id}}).First();
		}

		public Employee Manager
		{
			get
			{
				return Employee.Find((int) this["ManagerID"]);
			}
			set
			{
				if(!value.IsManager) throw new DatabaseException("Employee is not a manager.");
				this["ManagerID"] = value.ID;
			}
		}

		public Employee Salesperson
		{
			get { return Employee.Find((int)this["SalespersonID"]); }
			set { this["SalespersonID"] = value.ID; }
		}

		public string CustomerInfo
		{
			get { return (string) this["CustomerInfo"]; }
			set { this["CustomerInfo"] = value; }
		}

		public double Price
		{
			get { return (System.Single) this["Price"]; }
			set { this["Price"] = value; }
		}
	}
}

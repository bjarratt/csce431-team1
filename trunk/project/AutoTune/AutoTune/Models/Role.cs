using System.Linq;

namespace AutoTune.Models
{
	public class Role : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"EmployeeID", "DealershipID", "RoleTypeID"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "Roles"; }


	}
}

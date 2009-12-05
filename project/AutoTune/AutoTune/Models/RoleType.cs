using System.Linq;

namespace AutoTune.Models
{
	public class RoleType : DatabaseModel
	{
		private static readonly string[] DatabaseFields = {"Label"};
		public override bool IsDatabaseField(string fieldName)
		{ return DatabaseFields.Contains(fieldName); }

		public override string TableName()
		{ return "RoleTypes"; }
	}
}

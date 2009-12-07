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

		public static RoleType Find(int id)
		{ return Find(id); }

		public string Label
		{ get { return (string)this["Label"]; } }
	}
}

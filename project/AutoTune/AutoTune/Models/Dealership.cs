using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

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
			DatabaseModel[] models = Find("Dealerships", () => new Dealership(), conditions);
			Dealership[] dealerships = new Dealership[models.Length];
			for (int i = 0; i < models.Length; ++i)
				dealerships[i] = (Dealership)models[i];
			return dealerships;
		}
	}
}

using System;
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
	public class RoleType : DatabaseModel
	{
		private RoleType(string label)
		{Label=label;}

		public string Label;

		static RoleType Salesperson { get { return new RoleType("Salesperson"); } }
	}
}

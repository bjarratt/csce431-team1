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
	public abstract partial class DatabaseModel
	{
		public class DatabaseException : Exception
		{
			public DatabaseException(string message) :
				base(message) { }
		}
		private class InternalException : Exception
		{
			public InternalException(string message) :
				base(message) { }
		}
	}
}

using MySql.Data.MySqlClient;
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
		private MySqlConnection Connection = null;
		private static string ConnectionString = "datasource=database2.cs.tamu.edu;username=ironize;password=autotune;database=ironize";
		private void OpenConnection()
		{
			Connection = new MySqlConnection(ConnectionString);
			Connection.Open();
		}

		private void CloseConnection()
		{
			Connection.Close();
		}
	}
}

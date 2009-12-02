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

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

using MySql;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace AutoTune.Models
{
	public class Employee : DatabaseModel
	{
		public static string[] DatabaseFields = {"Username", "PasswordHash", "Salt", "TemporaryPassword"};
		public override bool IsDatabaseField(string field_name)
		{
			return DatabaseFields.Contains(field_name);
		}

		public override string TableName()
		{
			return "Employees";
		}

		public string Username;
		public string PasswordHash;
		public string Salt;
		public string TemporaryPassword;

		public static Employee Authenticate(string username, string password)
		{
			throw new NotImplementedException();
		}

		public static Regex UsernameRegex = new Regex("^\\w{6,32}$");
		public static bool IsValidUsername(string username)
		{
			return UsernameRegex.Match(username).Success;
		}

		public static Regex PasswordRegex = new Regex("^[^\\s]{6,32}$");
		public static bool IsValidPassword(string password)
		{
			return PasswordRegex.Match(password).Success;
		}
	}
}

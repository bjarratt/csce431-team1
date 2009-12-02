using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MySql;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AutoTune.Models
{
	public abstract class DatabaseModel
	{
		public class DatabaseException : Exception
		{
			public DatabaseException(string message) :
				base(message) { }
		}

		public abstract bool IsDatabaseField(string field_name);

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

		public static string GenerateNewSalt()
		{
			return Hash(String.Format("{0}{1}", DateTime.Now, (new Random()).NextDouble()));
		}

		public static string Hash(string value)
		{
			byte[] input = Encoding.UTF8.GetBytes(value);
			SHA1CryptoServiceProvider crypt = new SHA1CryptoServiceProvider();
			byte[] output = crypt.ComputeHash(input);
			string result = "";
			foreach (byte b in output)
			{
				result += b.ToString("X2");
			}
			return result;
		}

		public static bool IsValid(string value, string field_name, Type self)
		{
			FieldInfo field = self.GetField(field_name);

			throw new NotImplementedException();
		}

		public bool AllFieldsAreValid()
		{
			FieldInfo[] fields = this.GetType().GetFields();
			foreach (FieldInfo field in fields)
			{
				if (IsDatabaseField(field.Name))
				{
					Type self = this.GetType();

					object value = field.GetValue(this);

					FieldInfo fieldvalidator = self.GetField(field.Name + "Regex");
					if (fieldvalidator == null) continue;
					object validator = fieldvalidator.GetValue(this);

					if(!(value is string)) continue;	// Only validate string properties against regex

					if (validator is Regex)
					{
						Regex regex = (Regex)validator;
						if (!regex.Match((string)value).Success)
							return false;
					}
					else
					{
						throw new DatabaseException("Expected to find Regex for field validation.");
					}
				}
			}

			return true;
		}
	}
}
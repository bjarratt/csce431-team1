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
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AutoTune.Models
{
	public class Employee : DatabaseModel
	{
		public enum Type{All, Manager, Salesperson, Sysadmin};

		public int EmployeeID { get; private set; }
		public string Username;
		public string PasswordHash { get; private set; }
		public string Salt;

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

		public static Employee[] Find(RoleType type)
		{
			throw new NotImplementedException();
		}

		private static Employee Find(string username)
		{
			Employee[] all_employees = ListAll();
			foreach (Employee employee in all_employees)
			{
				if (employee.Username.Equals(username))
				{
					return employee;
				}
			}

			return null;
		}

		public static Employee Authenticate(string username, string password)
		{
			Employee user = Find(username);
			if (user != null && user.PasswordHash.Equals(Hash(password + user.Salt)))
			{
				return user;
			}
			else
			{
				return null;
			}
		}

		private static Employee[] ListAll()
		{
			string filename = "Employees.xml";

			FileStream file = new FileStream(filename, System.IO.FileMode.Open);
			XmlDocument doc = new XmlDocument();
			doc.Load(file);

			XmlNodeList xml_employees = doc.GetElementsByTagName("Employee");
			List<Employee> employees = new List<Employee>();
			foreach (XmlNode node in xml_employees)
			{
				employees.Add(new Employee(node));
			}

			file.Close();

			return employees.ToArray();
		}

		private Employee(XmlNode node)
		{
			EmployeeID = int.Parse(node.Attributes["EmployeeID"].Value);
			Username = node.Attributes["Username"].Value;
			PasswordHash = node.Attributes["PasswordHash"].Value;
			Salt = node.Attributes["Salt"].Value;
		}
	}
}

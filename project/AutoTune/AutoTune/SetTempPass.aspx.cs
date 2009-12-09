using System;
using System.Web.UI;
using AutoTune.Models;

namespace AutoTune
{
	public partial class SetTempPass : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = int.Parse(Request["id"]);
			Employee employee = Employee.Find(id);

			string tempPass = DatabaseModel.GenerateNewSalt().Substring(0, 5);
			employee.TemporaryHash = DatabaseModel.Hash(tempPass + employee.Salt);
			employee.Commit();

			UsernameLabel.Text = employee.Username;
			PasswordLabel.Text = tempPass;
		}
	}
}

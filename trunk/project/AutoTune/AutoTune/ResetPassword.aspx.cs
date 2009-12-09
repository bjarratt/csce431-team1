using System;
using System.Web.UI;
using AutoTune.Models;

namespace AutoTune
{
	public partial class ResetPassword : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void ResetButton_Click(object sender, EventArgs e)
		{
			string username = UsernameTextBox.Text;
			string tempPassword = TemporaryTextBox.Text;
			string password = PasswordTextBox.Text;
			string confirm = ConfirmTextBox.Text;
			Employee employee = Employee.FindByUsername(username);
			if(employee == null || employee.TemporaryHash == null)
			{
				WarningLabel.Text = "User was not found or has not been assigned a temporary password.";
			}
			else
			{
				string enteredHash = DatabaseModel.Hash(tempPassword + employee.Salt);
				string actualHash = employee.TemporaryHash;
				if(enteredHash == employee.TemporaryHash)
				{
					employee.Password = password;
					employee.TemporaryHash = null;
					employee.Commit();
				}
				else
				{
					WarningLabel.Text = "Incorrect temporary password. Please contact your system administrator.";
				}
			}
		}
	}
}

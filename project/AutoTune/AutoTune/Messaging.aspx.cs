using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using AutoTune.Models;
namespace AutoTune
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Employee user = (Employee)Session["User"];
            if (user == null)
                Response.Redirect("default.aspx");
            else
            {
                String m = "";

                IEnumerable<Message> messages = user.GetMessages();

                if (messages != null)
                {
                    if (messages.Count() <= 10)
                        foreach (Message message in messages)
                            m = m + message.Body + "\n";
                    else
                    {
                        for (int i = messages.Count() - 11; i < messages.Count(); i++)
                        {
                            Message message = messages.ElementAt(i);
                            string messageSender = message.Sender.Username;
                            string messageBody = message.Body;
                            m = m + string.Format(
                                "<b>From: {0}</b>:<br />Message:<br />{1}<br />",
                                messageSender, messageBody) + "\n";
                        }
                    }
                }

                MessagesBox.Text = m;
            }
        }

        protected void SendButton_Click(object sender, EventArgs e)
        {
            String MessageText = NewMessageBox.Text;

            Message newMessage = new Message();

            String[] names = (RecipientsBox.Text).Split(',');
            Employee[] recipients = new Employee[names.Count()];
            for(int i=0;i<names.Count();i++)
            {
                String name = names[i];
                Employee recipient = Employee.FindByUsername(name);
                recipients[i] = recipient;
            }
            
            newMessage.Body = MessageText;
            newMessage.Sender = (Employee)Session["User"];
            newMessage.Commit();
            newMessage.SendTo(recipients);
        }

        protected void MessagesBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

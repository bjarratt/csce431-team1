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
            update_messages();
            Employee user = (Employee)Session["User"];
            if (user != null)
            {
                Label1.Text = user.Username;
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void update_messages()
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
                    if (messages.Count() <= 3)
                        foreach (Message message in messages)
                        {
                            string messageSender = message.Sender.Username;
                            string messageBody = message.Body;

                            m = m + string.Format(
                                "<b>From: {0}</b><br />{1}<br /><br />",
                                messageSender, messageBody);
                        }
                    else
                    {
                        for (int i = messages.Count() - 3; i < messages.Count(); i++)
                        {
                            Message message = messages.ElementAt(i);
                            string messageSender = message.Sender.Username;
                            string messageBody = message.Body;
                            m = m + string.Format(
                                "<b>From: {0}</b><br />Message: {1}<br /><br />",
                                messageSender, messageBody);
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
            update_messages();
            NewMessageBox.Text = String.Empty;
            RecipientsBox.Text = String.Empty;
        }

        protected void MessageClick(object sender, EventArgs e)
        {
            Response.Redirect("Messaging.aspx");
        }
        private void Logout()
        {
            Session["User"] = null;
            Response.Redirect("Default.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        protected void MarkAsRead_Click(object sender, EventArgs e)
        {
            //delete messages here
            Employee user = (Employee)Session["User"];
            IEnumerable messages = user.GetMessages();

            foreach (Message message in messages)
            {
                message.Delete();
            }
            update_messages();
        }
    }
}

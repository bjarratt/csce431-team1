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
            String m = "";
            try
            {
                IEnumerable<Message> messages = user.GetMessages();

                if (messages.Count() <= 10)
                    foreach (Message message in messages)
                        m = m + message.Body + "\n";
                else
                {
                    for (int i = messages.Count() - 11; i < messages.Count(); i++)
                    {
                        Message message = messages.ElementAt(i);
                        m = m + message.Body + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            MessagesBox.Text = m;
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
            newMessage.Sender = User;
            newMessage.Commit();
            newMessage.SendTo(recipients);
        }
    }
}

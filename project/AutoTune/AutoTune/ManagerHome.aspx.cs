using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AutoTune
{
    public partial class ManagerHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void Logout()
        {
            Session["User"] = null;
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }
}

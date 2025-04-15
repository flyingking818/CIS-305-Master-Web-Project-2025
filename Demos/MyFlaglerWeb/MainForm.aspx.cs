using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CIS_305_Master_Web_Project.Demos.MyFlaglerWeb
{
    public partial class MainForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rblPersonType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Text = "OK";
        }
    }
}
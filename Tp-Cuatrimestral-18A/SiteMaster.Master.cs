using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_18A
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = System.IO.Path.GetFileName(Request.Path);

            if (currentPage.Equals("Default.aspx", StringComparison.OrdinalIgnoreCase))
            {
                navbarOptions.Visible = false;
            }
            else
            {
                navbarOptions.Visible = true;
            }
        }
    }
}
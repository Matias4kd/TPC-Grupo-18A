using Seguridad;
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

            if (Session["Usuario"] == null && !currentPage.Equals("Default.aspx", StringComparison.OrdinalIgnoreCase))
            {                
                Response.Redirect("Default.aspx");
                return;
            }

            if (currentPage.Equals("Default.aspx", StringComparison.OrdinalIgnoreCase))
            {
                navbarOptions.Visible = false;
                cerrarSesion.Visible = false;
            }
            else
            {
                navbarOptions.Visible = true;

                Usuario usuarioLogueado = (Usuario)Session["Usuario"];

                if (usuarioLogueado?.Rol != null && usuarioLogueado.Rol.RolId == 2)
                {
                    lblGestionUsuarios.Text = "Administrar Médicos";
                    lblEspecialidades.Visible = false;
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));

            HttpContext.Current.Session.Clear();

            Response.Redirect("Default.aspx", false);
        }
    }
}
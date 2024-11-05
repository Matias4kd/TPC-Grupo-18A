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
            // Obtiene el nombre de la página actual
            string currentPage = System.IO.Path.GetFileName(Request.Path);

            // Verifica si la página actual es "Default.aspx"
            if (currentPage.Equals("Default.aspx", StringComparison.OrdinalIgnoreCase))
            {
                // Oculta las opciones específicas en la barra de navegación para "Default.aspx"
                navbarOptions.Visible = false;
            }
            else
            {
                // Muestra las opciones en otras páginas
                navbarOptions.Visible = true;
            }
        }
    }
}
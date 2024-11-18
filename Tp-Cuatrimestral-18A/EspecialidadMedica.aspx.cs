using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaMedica {
    public partial class EspecialidadMedica : System.Web.UI.Page
    {
        EspecialidadNegocio negocio = new EspecialidadNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
            }
        }

        private void CargarEspecialidades()
        {
            gvEspecialidades.DataSource = negocio.Listar(); // Llamada al método adaptado
            gvEspecialidades.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreEspecialidad.Text.Trim();
            if (!string.IsNullOrEmpty(nombre))
            {
                negocio.Agregar(nombre);
                CargarEspecialidades();
                txtNombreEspecialidad.Text = string.Empty;
            }
        }

        protected void gvEspecialidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                negocio.EliminarLogicamente(id); // Llamada al método adaptado
                CargarEspecialidades();
            }
        }
    }

}
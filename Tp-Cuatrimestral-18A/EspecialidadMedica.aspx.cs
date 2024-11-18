using Dominio;
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
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
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
            List<Especialidad> especialidadesExistentes = new List<Especialidad>();
            EspecialidadNegocio Negocio = new EspecialidadNegocio();
            especialidadesExistentes = Negocio.Listar();

            if (!string.IsNullOrEmpty(nombre))
            {
                foreach (Especialidad item in especialidadesExistentes)
                {   
                    if(nombre == item.Nombre)
                    {
                        lblError.Text = "Especialidad ya existente";
                        lblError.Visible = true;
                        return;
                    }

                }
                negocio.Agregar(nombre);
                CargarEspecialidades();
                txtNombreEspecialidad.Text = string.Empty;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Button btnEliminar = (Button)sender;
            GridViewRow row = (GridViewRow)btnEliminar.NamingContainer;
            int id = int.Parse(btnEliminar.CommandArgument);

            negocio.EliminarLogicamente(id); // Actualiza el estado en la base de datos
            CargarEspecialidades();
        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            Button btnReactivar = (Button)sender;
            GridViewRow row = (GridViewRow)btnReactivar.NamingContainer;
            int id = int.Parse(btnReactivar.CommandArgument);

            negocio.ReactivarEspecialidad(id); // Actualiza el estado en la base de datos
            CargarEspecialidades();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {

            Response.Redirect("Pacientes.aspx");

        }


        
    }

}
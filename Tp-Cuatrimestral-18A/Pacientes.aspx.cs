using System;
using Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ClinicaMedica
{
    public partial class Pacientes : System.Web.UI.Page
    {
        private PacienteNegocio negocio = new PacienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPacientes();
            }
        }

        private void CargarPacientes()
        {
            gvPacientes.DataSource = negocio.Listar();
            gvPacientes.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtBuscarDNI.Text.Trim();
            gvPacientes.DataSource = negocio.Listar().FindAll(p => p.DNI == dni);
            gvPacientes.DataBind();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ModificarPaciente.aspx");
        }

            protected void btnAgregarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaPaciente.aspx");
        }


     
    }
}
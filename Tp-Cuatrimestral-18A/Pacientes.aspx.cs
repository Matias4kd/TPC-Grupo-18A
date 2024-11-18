using System;
using Negocio;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;


namespace ClinicaMedica
{
    public partial class Pacientes : System.Web.UI.Page
    {
        private PacienteNegocio negocio = new PacienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
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
            LinkButton btnModificar = (LinkButton)sender;
            string idPaciente = btnModificar.CommandArgument; // Obtiene el ID del paciente desde el CommandArgument
            Response.Redirect("ModificarPaciente.aspx?Id=" + idPaciente); // Pasa el ID en la URL
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEliminar = (LinkButton)sender;
                int idPaciente = Convert.ToInt32(btnEliminar.CommandArgument);

                PacienteNegocio negocio = new PacienteNegocio();
                negocio.EliminarPacienteYTurnos(idPaciente);

                lblMensaje.Text = "Paciente eliminado correctamente.";
                lblMensaje.CssClass = "text-success";
                CargarPacientes();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el paciente: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        protected void btnAgregarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaPaciente.aspx");
        }

        protected void linkSeleccionar_Click(object sender, EventArgs e)
        {

            LinkButton btnSeleccionar = (LinkButton)sender;
            int idPaciente = Convert.ToInt32(btnSeleccionar.CommandArgument);
            Paciente paciente = new Paciente();
            PacienteNegocio pacienteNegocio = new PacienteNegocio();
            paciente = pacienteNegocio.ObtenerPorID(idPaciente);
            Session.Add("PacienteSeleccionado", paciente);
            Response.Redirect("Medicos.aspx");

        }
    }
}
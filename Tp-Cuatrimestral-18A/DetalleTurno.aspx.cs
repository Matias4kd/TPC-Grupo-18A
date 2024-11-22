using Dominio;
using Negocio;
using Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tp_Cuatrimestral_18A
{
    public partial class DetalleTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                txtApellidoPaciente.Enabled = false;
                txtNombrePaciente.Enabled = false;
                txtFecha.Enabled = false;
                txtHorario.Enabled = false;

                Usuario usuarioLogueado = new Usuario();
                usuarioLogueado = (Usuario)Session["Usuario"];

                if(usuarioLogueado.Rol.RolId == 3)
                {
                    ddlEstado.Enabled = false;
                }

                int idTurno = int.Parse(Request.QueryString["IdTurno"]);

                Turno turno = new Turno();
                TurnoNegocio turnoNegocio = new TurnoNegocio();

                turno = turnoNegocio.BuscarPorID(idTurno);

                CargarEstados();
                CargarDatos(turno);
            }
        }

        private void CargarDatos(Turno turno)
        {
            txtNombrePaciente.Text = turno.Paciente.Nombre;
            txtApellidoPaciente.Text = turno.Paciente.Apellido;
            txtFecha.Text = turno.Fecha.ToString("dd/MM/yyyy");
            txtHorario.Text = turno.Hora.ToString(@"hh\:mm");

            foreach (ListItem item in ddlEstado.Items)
            {
                if (item.Text == turno.Estado)
                {
                    ddlEstado.SelectedValue = item.Value;
                    break;
                }
            }
            txtObservaciones.Text = turno.Observaciones;
        }

        private void CargarEstados()
        {
            ddlEstado.Items.Add(new ListItem("Nuevo", "1"));
            ddlEstado.Items.Add(new ListItem("Cancelado", "2"));
            ddlEstado.Items.Add(new ListItem("No Asistió", "3"));
            ddlEstado.Items.Add(new ListItem("Cerrado", "4"));
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string origen = Request.QueryString["origen"];

            if (origen == "AgendaMedico")
            {
                Medico medicoEnAgenda = new Medico();
                medicoEnAgenda = (Medico)Session["MedicoEnAgenda"];
                Response.Redirect("AgendaMedico.aspx?IdMedico= " + medicoEnAgenda.IdMedico);
            }
            else if (origen == "AgendaPaciente")
            {
                Paciente pacienteEnAgenda = new Paciente();
                pacienteEnAgenda = (Paciente)Session["PacienteEnAgenda"];
                Response.Redirect("AgendaPaciente.aspx?IdPaciente=" + pacienteEnAgenda.IdPaciente);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int idTurno = int.Parse(Request.QueryString["IdTurno"]);

            Turno turno = new Turno();
            TurnoNegocio turnoNegocio = new TurnoNegocio();

            turno = turnoNegocio.BuscarPorID(idTurno);

            turno.Estado = ddlEstado.SelectedItem.Text;
            turno.Observaciones = txtObservaciones.Text;

            turnoNegocio.Modificar(turno);

            string origen = Request.QueryString["origen"];

            if (origen == "AgendaMedico")
            {
                Medico medicoEnAgenda = new Medico();
                medicoEnAgenda = (Medico)Session["MedicoEnAgenda"];
                Response.Redirect("AgendaMedico.aspx?IdMedico= " + medicoEnAgenda.IdMedico);
            }
            else if (origen == "AgendaPaciente")
            {
                Paciente pacienteEnAgenda = new Paciente();
                pacienteEnAgenda = (Paciente)Session["PacienteEnAgenda"];
                Response.Redirect("AgendaPaciente.aspx?IdPaciente=" + pacienteEnAgenda.IdPaciente);
            }
        }
    }
}
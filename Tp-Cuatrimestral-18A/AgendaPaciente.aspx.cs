using ClinicaMedica;
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
    public partial class AgendaPaciente1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");         // Verificar sesion de usuario
            }

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario != null)
                {
                    Paciente paciente = new Paciente();
                    int idPaciente= int.Parse(Request.QueryString["IdPaciente"]);
                    PacienteNegocio pacienteNegocio= new PacienteNegocio();
                    paciente = pacienteNegocio.ObtenerPorID(idPaciente);
                    Session.Add("PacienteEnAgenda", paciente);

                    if (paciente != null)
                    {
                        lblAgendaPaciente.Text = $"Turnos de {paciente.Nombre} {paciente.Apellido}";
                        CargarTurnos(paciente);
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void CargarTurnos(Paciente paciente)
        {
            List<Turno> turnos = new List<Turno>();
            TurnoNegocio negocio = new TurnoNegocio();
            turnos = negocio.ListarTurnosPaciente(paciente);

            var turnosSimplificados = turnos.Select(t => new
            {
                IdTurno = t.IdTurno,
                NombreMedico = t.Medico.Nombres,
                ApellidoMedico = t.Medico.Apellidos,
                HorarioTurno = t.Hora.ToString(@"hh\:mm"),
                Observaciones = t.Observaciones,
                Estado = t.Estado
            }).ToList();

            gvTurnos.DataSource = turnosSimplificados;
            gvTurnos.DataBind();
        }

        protected void lnkVer_Click(object sender, EventArgs e)
        {
            LinkButton btnVer = (LinkButton)sender;
            int idTurno = Convert.ToInt32(btnVer.CommandArgument);
            Response.Redirect("DetalleTurno.aspx?IdTurno=" + idTurno + "&origen=AgendaPaciente");
        }
    }
}
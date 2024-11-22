using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using ClinicaMedica;
using Seguridad;
using System.Net.Mail;
using System.Web.Services.Description;
using System.Net;

namespace Tp_Cuatrimestral_18A
{
    public partial class TurnosMedico : System.Web.UI.Page
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

                if (Session["PacienteSeleccionado"] == null)
                {
                    Response.Redirect("Pacientes.aspx");                    
                }
                string idMedicoAnterior = Request.QueryString["IdMedico"];

                if (int.TryParse(idMedicoAnterior, out int idMedico))
                {
                    CargarDatosMedico(idMedico);

                    ddlTurnosDisponibles.Visible = false;
                }

            }


        }

        protected void btnAgendarTurno_Click(object sender, EventArgs e)
        {

            try
            {

                MedicoNegocio medicoNegocio = new MedicoNegocio();
                Paciente paciente = (Paciente)Session["PacienteSeleccionado"];
                int idMedico = int.Parse(Request.QueryString["IdMedico"]);
                Medico medico = new Medico();
                medico = medicoNegocio.ObtenerPorID(idMedico);
                Especialidad especialidad = (Especialidad)Session["EspecialidadTurno"];

                EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
                Turno turno = new Turno();

                turno.Paciente = paciente;
                turno.Medico = medico;
                turno.Especialidad = especialidad;
                turno.Fecha = calendarioTurnos.SelectedDate;
                turno.Hora = TimeSpan.Parse(ddlTurnosDisponibles.SelectedValue);
                turno.Observaciones = txtObservaciones.Text;

                TurnoNegocio turnoNegocio = new TurnoNegocio();
                bool turnoAgendado = turnoNegocio.AgendarTurno(turno);

                if (turnoAgendado)
                {

                    paciente = (Paciente)Session["PacienteSeleccionado"];
                    EnviarCorreoConfirmacion(paciente, turno);

                    pnlFormularioMedico.Visible = false;
                    pnlTurnoExitoso.Visible = true;
                    Session.Remove("PacienteSeleccionado");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            pnlFormularioMedico.Visible = false;
            pnlTurnoExitoso.Visible = true;
        }


        private void CargarDatosMedico(int idMedico)
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medico = medicoNegocio.ObtenerPorID(idMedico);

            if (medico != null)
            {
                lblNombreMedico.Text = medico.Nombres;
                lblApellidoMedico.Text = medico.Apellidos;
                Especialidad especialidad = (Especialidad)Session["EspecialidadTurno"];
                lblEspecialidadesMedico.Text = especialidad.Nombre;

            }
        }

        protected void calendarioTurnos_SelectionChanged(object sender, EventArgs e)
        {
            btnAgendarTurno.Visible = true;
            DateTime fechaSeleccionada = calendarioTurnos.SelectedDate;

            string idMedicoAnterior = Request.QueryString["IdMedico"];
            if (int.TryParse(idMedicoAnterior, out int idMedico))
            {
                TurnoNegocio tnegocio = new TurnoNegocio();
                List<TimeSpan> turnosDisponibles = tnegocio.turnosDisponibles(idMedico, fechaSeleccionada);

                ddlTurnosDisponibles.DataSource = turnosDisponibles;
                ddlTurnosDisponibles.DataBind();

                lblSeleccioneHorario.Visible = true;
                ddlTurnosDisponibles.Visible = true;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");
        }

        protected void calendarioTurnos_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void EnviarCorreoConfirmacion(Paciente paciente, Turno turno)
        {
            try
            {
                // Configurar mensaje de correo
                string asunto = "Confirmación de turno";
                string cuerpo = $@"
            Estimado paciente,

            Su turno ha sido confirmado exitosamente. Aquí están los detalles:
            - Fecha: {turno.Fecha.ToShortDateString()}
            - Horario: {turno.Hora}
            - Médico: {lblNombreMedico.Text} {lblApellidoMedico.Text}
            - Especialidad: {lblEspecialidadesMedico.Text}

            Por favor, conserve este correo como comprobante.

            Saludos,
            Clínica Médica UTN";

                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("clinicamedicautn@gmail.com");
                mensaje.To.Add(paciente.Email);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpo;
                mensaje.IsBodyHtml = false;

                SmtpClient smtpClient = new SmtpClient("sandbox.smtp.mailtrap.io")
                {
                    Port = 2525,
                    Credentials = new NetworkCredential("10cc991206f503", "fffd0c65d71d53"),
                    EnableSsl = true
                };

                smtpClient.Send(mensaje);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}



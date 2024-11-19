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
            if (!IsPostBack)
            {
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

                if (medico.Especialidades != null && medico.Especialidades.Count > 0)
                {
                    lblEspecialidadesMedico.Text = string.Join(", ", medico.Especialidades.Select(e => e.Nombre));
                }
                else
                {
                    lblEspecialidadesMedico.Text = "Sin especialidades";
                }
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
    }
}



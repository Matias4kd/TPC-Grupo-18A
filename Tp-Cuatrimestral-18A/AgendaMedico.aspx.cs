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
    public partial class AgendaMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");         // Verificar sesion de usuario
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario != null)
                {
                    Medico medico = new Medico();
                    int idMedico = int.Parse(Request.QueryString["IdMedico"]);
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    medico = medicoNegocio.ObtenerPorID(idMedico);
                    Session.Add("MedicoEnAgenda", medico);

                    if (medico != null)
                    {
                        if (usuario.Rol.RolId == 3)
                        {
                            lblAgenda.Text = $"Bienvenido, Dr./Dra. {medico.Nombres} {medico.Apellidos}";
                        }
                        else
                        {
                            lblAgenda.Text = $"Agenda del médico: Dr./Dra. {medico.Nombres} {medico.Apellidos}";
                        }
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }


        private void CargarTurnos(DateTime fecha)
        {
            Medico medico = new Medico();
            int idMedico = int.Parse(Request.QueryString["IdMedico"]);
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            medico = medicoNegocio.ObtenerPorID(idMedico);
            List<Turno> turnos = new List<Turno>();
            TurnoNegocio negocio = new TurnoNegocio();
            turnos = negocio.ListarTurnosMedico(medico, fecha);

            var turnosSimplificados = turnos.Select(t => new
            {
                IdTurno = t.IdTurno,
                NombrePaciente = t.Paciente.Nombre,
                ApellidoPaciente = t.Paciente.Apellido,
                HorarioTurno = t.Hora.ToString(@"hh\:mm"),
                Observaciones = t.Observaciones,
                Estado = t.Estado
            }).ToList();

            gvTurnos.DataSource = turnosSimplificados;
            gvTurnos.DataBind();
        }

        protected void txtFechaTurno_TextChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = new DateTime();
            fechaSeleccionada = DateTime.Parse(txtFechaTurno.Text);
            CargarTurnos(fechaSeleccionada);
        }

        protected void lnkVer_Click(object sender, EventArgs e)
        {
            LinkButton btnVer = (LinkButton)sender;
            int idTurno = Convert.ToInt32(btnVer.CommandArgument);
            Response.Redirect("DetalleTurno.aspx?IdTurno=" + idTurno + "&origen=AgendaMedico");
        }
    }
}
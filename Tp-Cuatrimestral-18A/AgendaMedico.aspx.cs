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
        Medico medico = new Medico();
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
                    int idMedico = int.Parse(Request.QueryString["IdMedico"] ?? "0");
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    medico = medicoNegocio.ObtenerPorID(idMedico);

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
            TurnoNegocio negocio = new TurnoNegocio();
            gvTurnos.DataSource = negocio.Listar(medico, fecha);
            gvTurnos.DataBind();
        }

        protected void txtFechaTurno_TextChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = new DateTime();
            fechaSeleccionada = DateTime.Parse(txtFechaTurno.Text);
            CargarTurnos(fechaSeleccionada);
        }
    }
}
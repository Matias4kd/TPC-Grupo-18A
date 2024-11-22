using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaMedica
{



    public partial class Medicos : Page
    {
        private PrepagaNegocio prepagaNegocio;
        private EspecialidadNegocio especialidadNegocio;
        private MedicoNegocio medicoNegocio;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            gvMedicos.RowDataBound += gvMedicos_RowDataBound;

            prepagaNegocio = new PrepagaNegocio();
            especialidadNegocio = new EspecialidadNegocio();
            medicoNegocio = new MedicoNegocio();

            if (!IsPostBack)
            {
                
                Paciente pacienteSeleccionado = (Paciente)Session["PacienteSeleccionado"];

                if (pacienteSeleccionado != null)
                {

                    CargarPrepagas();
                    ddlPrepagas.SelectedValue = pacienteSeleccionado.prepaga.IdPrepaga.ToString();
                   

                   
                } 

                CargarPrepagas();
                CargarEspecialidades();
                CargarMedicos();

            }
        }

        protected void gvMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkSeleccionar = (LinkButton)e.Row.FindControl("lnkSeleccionar");

                if (Session["PacienteSeleccionado"] == null)
                {
                    lnkSeleccionar.Enabled = false; 
                    lnkSeleccionar.CssClass += " disabled";
                }
            }
        }

        private void CargarPrepagas()
        {
            try
            {
                ddlPrepagas.DataSource = prepagaNegocio.Listar();
                ddlPrepagas.DataTextField = "Nombre";
                ddlPrepagas.DataValueField = "IdPrepaga";
                ddlPrepagas.DataBind();

   
                ddlPrepagas.Items.Insert(0, new ListItem("Seleccione una prepaga", ""));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar las prepagas: " + ex.Message);
            }
        }

        private void CargarEspecialidades()
        {
            try
            {
                ddlEspecialidades.DataSource = especialidadNegocio.ListarTodas();
                ddlEspecialidades.DataTextField = "Nombre";
                
                ddlEspecialidades.DataBind();

                
                ddlEspecialidades.Items.Insert(0, new ListItem("Seleccione una especialidad", ""));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar las especialidades: " + ex.Message);
            }
        }

        protected void ddlPrepagas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMedicos();
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMedicos();
        }

        private void CargarMedicos()
        {
            try
            {
                
                bool prepagaSeleccionada = ddlPrepagas.SelectedIndex > 0;
                bool especialidadSeleccionada = ddlEspecialidades.SelectedIndex > 0;

                if (prepagaSeleccionada && especialidadSeleccionada)
                {
             
                    gvMedicos.DataSource = medicoNegocio.ListarPorPrepagaYEspecialidad(
                        ddlPrepagas.SelectedItem.Text, ddlEspecialidades.SelectedItem.Text);

                }
                else if (especialidadSeleccionada)
                {
                    
                    gvMedicos.DataSource = medicoNegocio.ListarPorEspecialidad(ddlEspecialidades.SelectedItem.Text);
                }
                else if (prepagaSeleccionada)
                {
                   if(ddlPrepagas.SelectedItem.Text == "No tiene")
                   {
                       gvMedicos.DataSource = medicoNegocio.Listar();

                   }
                   else
                   {
                    gvMedicos.DataSource = medicoNegocio.ListarPorPrepaga(ddlPrepagas.SelectedItem.Text);
                   }
                }
                else
                {
                   
                    gvMedicos.DataSource = medicoNegocio.Listar();
                }

                gvMedicos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los médicos: " + ex.Message);
            }
        }

        protected void lnkSeleccionar_Command(object sender, CommandEventArgs e)
        {
            if (ddlEspecialidades.SelectedIndex == 0)
            {
                lblEspecialidadError.Text = "Debe seleccionar una especialidad.";

            }
            else { 


                int idMedico = Convert.ToInt32(e.CommandArgument);
                string NombreEspecialidad = ddlEspecialidades.SelectedItem.Text;
                Especialidad especialidad = new Especialidad();
                especialidad = especialidadNegocio.BuscarEspecialidadesPorNombre(NombreEspecialidad);
                Session.Add("EspecialidadTurno", especialidad);

                Response.Redirect("TurnosMedico.aspx?IdMedico=" + idMedico);

            }
        }

        protected void lnkAgenda_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int idMedico = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("AgendaMedico.aspx?IdMedico=" + idMedico);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al redirigir a la agenda del médico: " + ex.Message);
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            litMensaje.Text = mensaje;
            pnlMensajes.Visible = true;
        }
    }

}


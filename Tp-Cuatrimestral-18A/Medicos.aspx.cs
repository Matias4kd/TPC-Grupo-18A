using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
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

                    //Chequear que solo te levante paciente en sesion si venis de la pag de pacientes.
                }
                CargarPrepagas();
                CargarEspecialidades();
                CargarMedicos();

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
                   
                    gvMedicos.DataSource = medicoNegocio.ListarPorPrepaga(ddlPrepagas.SelectedItem.Text);
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
            int idMedico = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("TurnosMedico.aspx?IdMedico=" + idMedico);
        }

        private void MostrarMensaje(string mensaje)
        {
            litMensaje.Text = mensaje;
            pnlMensajes.Visible = true;
        }
    }

}


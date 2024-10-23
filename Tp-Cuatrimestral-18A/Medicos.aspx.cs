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
                CargarPrepagas();
                // Inicialmente el dropdown de especialidades estará vacío hasta que se seleccione una prepaga
                ddlEspecialidades.Items.Clear();
                ddlEspecialidades.Items.Insert(0, new ListItem("Seleccione una prepaga primero", ""));
                ddlEspecialidades.Enabled = false;
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

        protected void ddlPrepagas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPrepagas.SelectedValue))
            {
                ddlEspecialidades.Items.Clear();
                ddlEspecialidades.Items.Insert(0, new ListItem("Seleccione una prepaga primero", ""));
                ddlEspecialidades.Enabled = false;
                return;
            }

            try
            {
                string nombrePrepaga = ddlPrepagas.SelectedItem.Text;
                ddlEspecialidades.DataSource = especialidadNegocio.ListarPorPrepaga(nombrePrepaga);
                ddlEspecialidades.DataTextField = "Nombre";
                ddlEspecialidades.DataValueField = "IdEspecialidad";
                ddlEspecialidades.DataBind();

                ddlEspecialidades.Items.Insert(0, new ListItem("Seleccione una especialidad", ""));
                ddlEspecialidades.Enabled = true;

                // Limpiar el GridView cuando se cambia la prepaga
                gvMedicos.DataSource = null;
                gvMedicos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar las especialidades: " + ex.Message);
            }
        }

        protected void ddlEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPrepagas.SelectedValue) ||
                string.IsNullOrEmpty(ddlEspecialidades.SelectedValue))
            {
                return;
            }

            try
            {

                string nombrePrepaga = ddlPrepagas.SelectedItem.Text;
                string nombreEspecialidad = ddlEspecialidades.SelectedItem.Text;
                gvMedicos.DataSource = medicoNegocio.ListarPorPrepagaYEspecialidad(nombrePrepaga, nombreEspecialidad);
                gvMedicos.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los médicos: " + ex.Message);
            }
        }

        private void CargarMedicos()
        {
           
            gvMedicos.DataSource = medicoNegocio.Listar();
            gvMedicos.DataBind();
        }

        protected void lnkSeleccionar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                int idMedico = Convert.ToInt32(e.CommandArgument);
                // Aquí puedes redirigir a la página de turnos o realizar la acción que necesites
                Response.Redirect($"Turnos.aspx?idMedico={idMedico}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            litMensaje.Text = mensaje;
            pnlMensajes.Visible = true;
        }
    }
}
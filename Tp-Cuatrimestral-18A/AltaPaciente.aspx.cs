using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicaMedica
{
    public partial class AltaPaciente : System.Web.UI.Page
    {
        private PacienteNegocio negocio = new PacienteNegocio();
       

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
                CargarPrepagas();
            }
        }


        private void CargarPrepagas()
        {
            PrepagaNegocio prepagaNegocio = new PrepagaNegocio();

            try
            {
                List<Prepaga> listaPrepagas = prepagaNegocio.Listar();

                ddlPrepaga.DataSource = listaPrepagas;
                ddlPrepaga.DataTextField = "Nombre"; 
                ddlPrepaga.DataValueField = "IdPrepaga"; 
                ddlPrepaga.DataBind();

                ddlPrepaga.Items.Insert(0, new ListItem("Seleccionar Prepaga", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    

    protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Paciente nuevoPaciente = new Paciente
            {
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                DNI = txtDNI.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                prepaga = new Prepaga { IdPrepaga = int.Parse(ddlPrepaga.SelectedValue) },
                FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text)
            };

            negocio.Agregar(nuevoPaciente);
            Response.Redirect("Pacientes.aspx");
        }


        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");


        }
    }
}
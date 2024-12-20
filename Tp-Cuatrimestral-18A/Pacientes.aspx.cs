﻿using System;
using Negocio;
using Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;


namespace ClinicaMedica
{
    public partial class Pacientes : System.Web.UI.Page
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
                CargarPacientes();
            }
        }

        private void CargarPacientes()
        {
            gvPacientes.DataSource = negocio.Listar();
            gvPacientes.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string dni = txtBuscarDNI.Text.Trim();
            if (string.IsNullOrEmpty(dni))
            {
                lblBuscar.Visible = true;
                lblBuscar.Text = "Debe ingresar un DNI para buscar";
            }
            else
            {
                gvPacientes.DataSource = negocio.Listar().FindAll(p => p.DNI == dni);
                gvPacientes.DataBind();
            }
        }

        

             protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pacientes.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            LinkButton btnModificar = (LinkButton)sender;
            string idPaciente = btnModificar.CommandArgument;
            Response.Redirect("ModificarPaciente.aspx?Id=" + idPaciente); 
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnEliminar = (LinkButton)sender;
                int idPaciente = Convert.ToInt32(btnEliminar.CommandArgument);

                PacienteNegocio negocio = new PacienteNegocio();
                negocio.EliminarPacienteYTurnos(idPaciente);


                CargarPacientes();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al eliminar el paciente: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        protected void btnAgregarPaciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaPaciente.aspx");
        }

        protected void linkSeleccionar_Click(object sender, EventArgs e)
        {

            LinkButton btnSeleccionar = (LinkButton)sender;
            int idPaciente = Convert.ToInt32(btnSeleccionar.CommandArgument);

            PacienteNegocio pacienteNegocio = new PacienteNegocio();
            Paciente paciente = pacienteNegocio.ObtenerPorID(idPaciente);
            
            Session["PacienteSeleccionado"] = paciente;

            Response.Redirect("Medicos.aspx");

        }

        protected void lnkAgenda_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int idPaciente = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("AgendaPaciente.aspx?IdPaciente=" + idPaciente);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al redirigir a la agenda del paciente: " + ex.Message);
            }
        }
        private void MostrarMensaje(string mensaje)
        {
            litMensaje.Text = mensaje;
            pnlMensajes.Visible = true;
        }
    }
}
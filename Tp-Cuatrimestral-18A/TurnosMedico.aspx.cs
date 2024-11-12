﻿using System;
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

            if (!IsPostBack)
            {
                string idMedicoAnterior = Request.QueryString["IdMedico"];
                if (int.TryParse(idMedicoAnterior, out int idMedico))
                {
                    TurnoNegocio tnegocio = new TurnoNegocio();
                    CargarDatosMedico(idMedico);


                    // mover a evento que detecta un cambio en la seleccion de la fecha
                    List<TimeSpan> turnosDisponibles = new List<TimeSpan>();
                    //tomar el valor de la fecha
                    turnosDisponibles = tnegocio.turnosDisponibles(idMedico, DateTime.Today); //reemplazar el datetime.today por el valor de la fecha
                    ddlTurnosDisponibles.DataSource = turnosDisponibles;
                    ddlTurnosDisponibles.DataBind();
                    //

                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            int dni;

            if (int.TryParse(txtDni.Text, out dni))
            {
                txtDniFormulario.Text = txtDni.Text;
                PacienteNegocio negociopaciente = new PacienteNegocio();
                Paciente paciente = negociopaciente.ObtenerPorDNI(dni);

                if (paciente != null)
                {
                    pnlFormularioPaciente.Visible = true;
                    txtNombre.Text = paciente.Nombre;
                    txtApellido.Text = paciente.Apellido;
                    txtEmail.Text = paciente.Email;
                    txtTelefono.Text = paciente.Telefono;
                    txtDireccion.Text = paciente.Direccion;
                    txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("yyyy-MM-dd");
                }
                else
                {
                    pnlFormularioPaciente.Visible = true;
                    LimpiarCampos();
                }
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtFechaNacimiento.Text = "";
        }

        protected void btnAgendar_Click(object sender, EventArgs e)
        {
            // Código para guardar o actualizar los datos del paciente en la base de datos
        }

        protected void btnAceptarHorario_Click(object sender, EventArgs e)
        {
            pnlFormularioMedico.Visible = false;
            pnlDni.Visible = true;
        }

        private void CargarDatosMedico(int idMedico)
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medico = medicoNegocio.ObtenerPorID(idMedico);

            if (medico != null)
            {
                lblNombreMedico.Text = medico.Nombres;
                lblApellidoMedico.Text = medico.Apellidos;
                lblMatriculaMedico.Text = medico.Matricula;
            }
        }
    }
}



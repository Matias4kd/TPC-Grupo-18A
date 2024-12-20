﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        public SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection("server = .\\SQLEXPRESS; database = ClinicaMedica_DB; integrated security = true");
            comando = new SqlCommand();


        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { comando.Parameters.Clear(); }


        }

        public void SetearSPTurnos(string sp,int idMedico, DateTime fecha)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
            comando.Parameters.AddWithValue("@IdMedico", idMedico);
            comando.Parameters.AddWithValue("@Fecha", fecha);

        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                comando.Parameters.Clear();
                conexion.Close();
            }

        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public object ejecutarEscalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar(); // Devuelve el primer valor de la primera fila
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close(); // Asegurarse de cerrar la conexión después de ejecutar
            }
        }

    }
}

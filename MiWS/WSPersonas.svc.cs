
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MiWS
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class WSPersonas : IWSPersonas
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
        public Personas buscarPersona(int idPersona)
        {
            Personas persona = new Personas();

            try
            {
                SqlConnection cnn = new SqlConnection(cadenaConexion);
                cnn.Open();

                SqlCommand cmd = new SqlCommand("_eliminarOListarPersona", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Operacion", "S");
                cmd.Parameters.AddWithValue("Id", idPersona);

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    if (rd.Read())
                    {
                        persona.Id = rd.GetInt32(0);
                        persona.Nombre = rd.GetString(1);
                        persona.Apellido = rd.GetString(2);
                        persona.Edad = rd.GetInt32(3);
                        persona.DocumentoIdentificacion = rd.GetString(4);
                    }
                }
                else
                {
                    throw new Exception("No hay registros");
                }
                cnn.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al buscar", ex);
            }

            return persona;
        }

        public int EditarPersona(Personas persona)
        {
            int res = 0;

            try
            {
                SqlConnection cnn = new SqlConnection(cadenaConexion);
                cnn.Open();

                SqlCommand cmd = new SqlCommand("_insertarPersonas", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Operacion", "A");
                cmd.Parameters.AddWithValue("Id", persona.Id);
                cmd.Parameters.AddWithValue("Nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("Apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("Edad", persona.Edad);
                cmd.Parameters.AddWithValue("DUI", persona.DocumentoIdentificacion);

                res = cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al actualizar", ex);
            }

            return res;
        }

        public int EliminarPersona(int idPersona)
        {
            int res = 0;

            try
            {
                SqlConnection cnn = new SqlConnection(cadenaConexion);
                cnn.Open();

                SqlCommand cmd = new SqlCommand("_eliminarOListarPersona", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Operacion", "E");
                cmd.Parameters.AddWithValue("Id", idPersona);

                res = cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al eliminar", ex);
            }

            return res;
        }

        public List<Personas> listarPersonas()
        {
            List<Personas> personas = new List<Personas>();

            try
            {
                SqlConnection cnn = new SqlConnection(cadenaConexion);
                cnn.Open();

                SqlCommand cmd = new SqlCommand("_eliminarOListarPersona", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Operacion", "L");

                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while(rd.Read())
                    {
                        personas.Add(new Personas
                        {
                            Id = rd.GetInt32(0),
                            Nombre = rd.GetString(1),
                            Apellido = rd.GetString(2),
                            Edad = rd.GetInt32(3),
                            DocumentoIdentificacion = rd.GetString(4)
                        });
                    }
                }
                else
                {
                    throw new Exception("No hay registros");
                }
                cnn.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al buscar", ex);
            }

            return personas;
        }

        public int NuevaPersona(Personas persona)
        {
            int res = 0;

            try
            {
                SqlConnection cnn = new SqlConnection(cadenaConexion);
                cnn.Open();

                SqlCommand cmd = new SqlCommand("_insertarPersonas", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Operacion", "I");
                cmd.Parameters.AddWithValue("Nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("Apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("Edad", persona.Edad);
                cmd.Parameters.AddWithValue("DUI", persona.DocumentoIdentificacion);

                res = cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al insertar", ex);
            }

            return res;
        }
    }
}

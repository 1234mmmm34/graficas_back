using API_Archivo.Clases;
using CardManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Reflection.PortableExecutable;

namespace API_Archivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Deudas_UsuarioController : ControllerBase
    {


        [HttpGet]
        [Route("Consultar_Deudores")]

        public List<Deudoress> Consultar_Deudores(int id_fraccionamiento)
        {



            List<Deudoress> Deuda = new List<Deudoress>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM deudas_ordinarias", conexion);

                //   comando.Parameters.Add("@id_tesorero", MySqlDbType.Int32).Value = id_tesorero;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Deuda.Add(new Deudoress()
                        {
                            id_deuda = reader.IsDBNull(0) ? 0 : reader.GetInt32(0), // Si es nulo, se asigna un valor predeterminado (en este caso, 0)
                            //concepto = reader.IsDBNull(7) ? string.Empty : reader.GetString(7), // Si es nulo, se asigna una cadena vacía
                            persona = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                            monto = reader.IsDBNull(5) ? 0.0f : reader.GetFloat(5), // Si es nulo, se asigna un valor flotante predeterminado (en este caso, 0.0)
                            proximo_pago = reader.IsDBNull(8) ? DateTime.MinValue : reader.GetDateTime(8)

                        });
                        // MessageBox.Show();
                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return Deuda;
            }

        }


        [HttpGet]
        [Route("Restringir_acceso")]
        public bool Eliminar_Persona(int id_deuda)
        {
            bool Persona_eliminada = false;
            int id_persona;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("SELECT id_persona FROM deudas_ordinarias WHERE id_deuda=@id_deuda", conexion);

                //@Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @Tipo_usuario, @id_fraccionamiento, @id_lote, @Intercomunicador, @Codigo_acceso

                // comando.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = nombre;
                //  comando.Parameters.Add("@Apellido_pat", MySqlDbType.VarChar).Value = apellido_paterno;
                comando.Parameters.Add("@id_deuda", MySqlDbType.Int32).Value = id_deuda;




                try
                {
                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {

                        Persona_eliminada = true;
                        id_persona = reader.GetInt32(0);
                        AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");
                        AddDevice.DeleteCardUser(id_persona.ToString());

                    }



                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                }
                return Persona_eliminada;

            }
        }

    }
}
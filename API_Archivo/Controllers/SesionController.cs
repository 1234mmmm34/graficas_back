/*
using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace API_Archivo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SesionController
    {

        [HttpGet]
        [Route("Iniciar_Sesion")]


        public List<Sesion> Iniciar_Sesion(string correo, string contrasenia)
        {
            List<Sesion> list_sesion = new List<Sesion>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("SELECT * from personas WHERE Correo = @Correo && Contrasenia = @Contrasenia", conexion);

                //@id_fraccionamiento, @Nombre_deuda, @Descripción, @Monto, @Fecha_corte, @Periodicidad_dias

                comando.Parameters.Add("@Correo", MySqlDbType.VarChar).Value = correo;
                comando.Parameters.Add("@Contrasenia", MySqlDbType.VarChar).Value = contrasenia;


                try
                {
                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    //    if(AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73") == true)
                    //    {
                    while (reader.Read())
                    {
                        list_sesion.Add(new Sesion() { correo = reader.GetString(1), id_usuario = reader.GetInt32(0), tipo_usuario = reader.GetString(13) });
                        // AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");
                    }

                    //    }


                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                }
                return list_sesion;
            }

        }


    }
}
*/


using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;

namespace API_Archivo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SesionController
    {

        [HttpGet]
        [Route("Iniciar_Sesion")]


        public List<Sesion> Iniciar_Sesion(string correo, string contrasenia)
        {
            List<Sesion> list_sesion = new List<Sesion>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("SELECT * from personas WHERE Correo = @Correo && Contrasenia = @Contrasenia", conexion);

                //@id_fraccionamiento, @Nombre_deuda, @Descripción, @Monto, @Fecha_corte, @Periodicidad_dias

                comando.Parameters.Add("@Correo", MySqlDbType.VarChar).Value = correo;
                comando.Parameters.Add("@Contrasenia", MySqlDbType.VarChar).Value = contrasenia;


                try
                {
                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    //    if(AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73") == true)
                    //    {
                    while (reader.Read())
                    {
                        list_sesion.Add(new Sesion() { correo = reader.GetString(1), id_usuario = reader.GetInt32(0), tipo_usuario = reader.GetString(13), id_fraccionamiento = reader.IsDBNull(6) ? 0 : reader.GetInt32(6), id_lote = reader.IsDBNull(7) ? 0 : reader.GetInt32(7) });
                        // AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");
                    }

                    //    }


                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                }

                //Deudas obj_deudas = new Deudas();

                //obj_deudas.AsignarDeudasAUltimaPersona(list_sesion[0].id_fraccionamiento);
                return list_sesion;
            }

        }


    }
}

using API_Archivo.Clases;
using CardManagement;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System.Data;

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
            string frac;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                DataTable dt = new DataTable();

                string ip = "";

                //  string ip="", port="", user = "", password = "";

                MySqlCommand comando = new MySqlCommand("sp_login", conexion);

                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@pass", contrasenia);

                try
                {
                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    dt.Load(reader);


                    int tesorero = 0;
                    bool var = false;

                    foreach (DataRow row in dt.Rows)
                    {
                        tesorero = Obtener_Tesorero(row.Field<int>("id_persona"));
                        ip = row.IsNull("ip") ? "" : row.Field<string>("ip");

                        if (ip != "")
                        {
                            var = AddDevice.Login(row.Field<string>("user"), row.Field<string>("password"), row.Field<string>("port"), row.Field<string>("ip"));
                        }

                        list_sesion.Add(new Sesion()
                        {
                            id_usuario = row.Field<int>("id_persona"),
                            correo = row.Field<string>("correo"),
                            tipo_usuario = row.Field<string>("tipo_usuario"),
                            id_fraccionamiento = row.IsNull("id_fraccionamiento") ? 0 : row.Field<int>("id_fraccionamiento"),
                            id_lote = row.IsNull("id_lote") ? 0 : row.Field<int>("id_lote"),
                            fraccionamiento = row.IsNull("codigo_acceso") ? "" : row.Field<string>("codigo_acceso"),
                            id_tesorero = tesorero,
                            nombre = row.Field<string>("nombre"),
                            //  ip = row.Field<string>("ip"),
                            //  port = row.Field<string>("port"),
                            //  password = row.Field<string>("password"),
                            //  user = row.Field<string>("user"),
                            conexion = var

                        });

                        /*
                        ip = row.Field<string>("ip");
                        port = row.Field<string>("port");
                        password = row.Field<string>("password");
                        user = row.Field<string>("user");
                        */

                        // AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");
                    }


                }
                catch (MySqlException ex)
                {
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    conexion.Close();
                    //  AddDevice.Login(user, password, port, ip);
                }

                return list_sesion;
            }

        }

        [HttpGet]
        [Route("Obtener_Tesorero")]
        public int Obtener_Tesorero(int id_administrador)
        {
            int tesorero = 0;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT id_persona FROM personas WHERE id_fraccionamiento=@id_administrador && tipo_usuario='tesorero'", conexion);

                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = id_administrador;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        tesorero = reader.GetInt32(0);
                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return tesorero;
            }
        }
    }


}

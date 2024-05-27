 using API_Archivo.Clases;
using CardManagement;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class HikvisionController
    {

        [HttpPost]
        [Route("Agregar_Hikvision")]

        public bool InsertHikvision(int id_controlador, int id_fraccionamiento, string user, string password, string port, string ip)
        {

            bool res = AddDevice.Login(user, password, port, ip);
            // bool res = AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");

            if (res == true)
            {
                using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
                {
                    //  res = false;
                    int rowsaffected = 0;

                    MySqlCommand comando = new MySqlCommand("select id_fraccionamiento from controlador where id_fraccionamiento = @id_fraccionamiento;", conexion);
                    comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                    try
                    {

                        conexion.Open();

                        MySqlDataReader reader = comando.ExecuteReader();

                        if (reader.Read())
                        {
                            res = false;
                        }


                    }
                    catch (MySqlException ex)
                    {
                        //MessageBox.Show(ex.ToString());
                    }


                    conexion.Close();
                    if (res == true)
                    {

                        conexion.Open();
                        res = false;
                        comando = new MySqlCommand("insert into controlador (id_fraccionamiento, user, password, port, ip) values(@id_fraccionamiento, @user, @password, @port, @ip);", conexion);

                    //    comando.Parameters.Add("@id_controlador", MySqlDbType.Int32).Value = id_controlador;
                        comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                        comando.Parameters.Add("@user", MySqlDbType.VarChar).Value = user;
                        comando.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
                        comando.Parameters.Add("@port", MySqlDbType.VarChar).Value = port;
                        comando.Parameters.Add("@ip", MySqlDbType.VarChar).Value = ip;


                        try
                        {
                            //  conexion.Open();
                            rowsaffected = comando.ExecuteNonQuery();

                            if (rowsaffected >= 1)
                            {
                                res = true;
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
                    }


                }

            }

            return res;
        }


        [HttpGet]
        [Route("Consultar_Hikvision")]

        public List<Hikvision> Consultar_Hikvision(int id_fraccionamiento)
        {

            List<Hikvision> Lista_acuerdos = new List<Hikvision>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM controlador WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_acuerdos.Add(new Hikvision()
                        {
                            user = reader.GetString(2),
                            password = reader.GetString(3),
                            port = reader.GetString(4),
                            ip = reader.GetString(5)

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

                return Lista_acuerdos;
            }


        }
    }
    }


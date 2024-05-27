using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using API_Archivo.Clases;

namespace API_Archivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerGlobal : ControllerBase
    {

        [HttpGet]
        [Route("Calcular_Registros")]
         
        public int calcular_registros(int id_fraccionamiento, string tabla)
        {

            int cantidadRegistros = 0;

            using (MySqlConnection conexion1 = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT COUNT(*) as cantidad_registros FROM "+tabla+" WHERE id_fraccionamiento=@id_fraccionamiento;", conexion1);

              
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {
                    conexion1.Open();

                    MySqlDataReader reader = comando.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        cantidadRegistros = reader.GetInt32(0);
                    }

                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion1.Close();


                }


                return cantidadRegistros;
            }

        }


    }
}

 
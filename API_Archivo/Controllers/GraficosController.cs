using API_Archivo.Clases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace API_Archivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraficosController : ControllerBase
    {

        [HttpGet]
        [Route("Consultar_DeudasPorCobrar")]

        public List<Graficos> Consultar_DeudasPorCobrar(int id_fraccionamiento)
        {

            List<Graficos> Graficos = new List<Graficos>();
            List<Graficos> Graficos1 = sum_variables(id_fraccionamiento);


            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                double suma = 0;

                MySqlCommand comando = new MySqlCommand("SELECT * FROM deudores WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        //   string fechaProximoPago = (reader.GetDateTime(9)).ToString("yyyy-MM-ddTHH:mm:ss");

                        DateTime fechaActual = reader.GetDateTime(11);
                        DateTime fechaNueva = fechaActual.AddDays(reader.GetInt32(10));

                        int comparacion = DateTime.Now.CompareTo(fechaNueva);

                        suma += reader.GetFloat(8);

                        if (comparacion > 0)
                        {
                            suma += reader.GetFloat(9);
                        }


                        // MessageBox.Show();
                    }

                    foreach (var item in Graficos1)
                    {

                        Graficos.Add(new Graficos()
                        {
                            cuentas_cobrar = suma,
                            sum_novariables = item.sum_novariables,
                            novariables = item.novariables,
                            sum_variables = item.sum_variables,
                            variables = item.variables,
                            por_variables = Math.Round(item.por_variables,2),
                            por_novariables = Math.Round(item.por_novariables, 2)

                        });


                    }
                }

                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return Graficos;
            }

        }

        static public List<Graficos> sum_variables(int id_fraccionamiento)
        {
            List<Graficos> Graficos = new List<Graficos>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int sum_variables = 0, sum_novariables=0;
                double variables = 0, novariables=0;

                MySqlCommand comando = new MySqlCommand("SELECT * FROM historial_deudaspagadas WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        //   string fechaProximoPago = (reader.GetDateTime(9)).ToString("yyyy-MM-ddTHH:mm:ss");


                        if (reader.GetString(6)=="ordinaria")
                        {
                            sum_novariables++;
                            novariables += reader.GetFloat(8) + reader.GetFloat(9);
                        }
                        else if(reader.GetString(6)=="extraordinaria")
                        {
                            sum_variables++;
                            variables += reader.GetFloat(8) + reader.GetFloat(9);
                        }

                    }


                    Graficos.Add(new Graficos()
                    {
                        sum_novariables = sum_novariables,
                        novariables = novariables,
                        sum_variables = sum_variables,
                        variables = variables,
                        por_variables = (variables/(variables+novariables))*100,
                        por_novariables = (novariables / (variables + novariables)) * 100,

                    });


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return Graficos;
            }
        }
    }
}

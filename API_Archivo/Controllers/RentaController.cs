using API_Archivo.Clases;
using CardManagement;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentaController : ControllerBase
    { 

        [HttpPost]
        [Route("Agregar_Arrendatario")]


        public bool Agregar_Arrendatario(int id_renta, int id_usuario, int id_fraccionamiento, int id_lote, int proximo_pago, float monto)
        {
            bool Agregar_Arrendatario = false;

            DateTime now = DateTime.Now;
            DateTime Dateproximo_pago = DateTime.Now.AddDays(proximo_pago);
            string fechaProximoPago = Dateproximo_pago.ToString("yyyy-MM-ddTHH:mm:ss");
            string fechaActual = now.ToString("yyyy-MM-ddTHH:mm:ss");

            Console.WriteLine(fechaActual);
            Console.WriteLine(fechaProximoPago);



            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into renta ( id_Persona, id_fraccionamiento, id_lote, monto) VALUES (@id_Persona, @id_fraccionamiento, @id_lote, @monto)", conexion);

                //comando.Parameters.Add("@id_renta", MySqlDbType.Int32).Value = id_renta;
                comando.Parameters.Add("@id_Persona", MySqlDbType.Int32).Value = id_usuario;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = id_lote;
                //  comando.Parameters.Add("@Dateproximo_pago", MySqlDbType.DateTime).Value = Dateproximo_pago;
                comando.Parameters.Add("@monto", MySqlDbType.Float).Value = monto;

                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Agregar_Arrendatario = true;
                        AddDevice.InsertUser(id_usuario.ToString(), id_usuario.ToString(), fechaActual, fechaProximoPago);
                      //  AddDevice.InsertCardUser(id_usuario.ToString());

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
                return Agregar_Arrendatario;

            }
        }



    }
}

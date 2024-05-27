using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace API_Archivo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcuerdosController : ControllerBase
    {

        [HttpPost]
        [Route("Agregar_Acuerdo")]


        public bool Agregar_Acuerdo(int id_fraccionamiento, string asunto, string detalles, string fecha)
        {
            bool Acuerdo_agregado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into acuerdos (id_fraccionamiento, asunto, detalles, fecha) VALUES (@id_fraccionamiento, @asunto, @detalles, @fecha)", conexion);

                //id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, Destinatario=@Destinatario, Asunto=@Asunto, Mensaje=@Mensaje

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@asunto", MySqlDbType.VarChar).Value = asunto;
                comando.Parameters.Add("@detalles", MySqlDbType.VarChar).Value = detalles;
                comando.Parameters.Add("@fecha", MySqlDbType.VarChar).Value = fecha;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Acuerdo_agregado = true;
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
                return Acuerdo_agregado;
            }
        }

        [HttpDelete]
        [Route("Eliminar_Acuerdo")]

        public bool Eliminar_Acuerdo(int id_acuerdo, int id_fraccionamiento)
        {

            bool Acuerdo_eliminado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM acuerdos WHERE id_acuerdo=@id_acuerdo && id_fraccionamiento=@id_fraccionamiento", conexion);

                //@id_usuario, @Tipo_deuda,@Nombre_deuda, @Monto, @Ruta_comprobante, @Estado

                comando.Parameters.Add("@id_acuerdo", MySqlDbType.Int32).Value = id_acuerdo;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Acuerdo_eliminado = true;
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
                return Acuerdo_eliminado;

            }
        }

        [HttpPatch]
        [Route("Actualizar_Acuerdo")]

        public bool Actualizar_Acuerdo(int id_acuerdo, int id_fraccionamiento, string asunto, string detalles, string fecha)
        {
            bool Acuerdo_actualizado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE acuerdos " +
                    "SET id_fraccionamiento=@id_fraccionamiento, asunto=@asunto, detalles=@detalles, fecha=@fecha " +
                    "WHERE id_acuerdo=@id_acuerdo", conexion);
                //id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, Destinatario=@Destinatario, Asunto=@Asunto, Mensaje=@Mensaje

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@asunto", MySqlDbType.VarChar).Value = asunto;
                comando.Parameters.Add("@detalles", MySqlDbType.VarChar).Value = detalles;
                comando.Parameters.Add("@fecha", MySqlDbType.VarChar).Value = fecha;
                comando.Parameters.Add("@id_acuerdo", MySqlDbType.Int32).Value = id_acuerdo;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Acuerdo_actualizado = true;
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
                return Acuerdo_actualizado;
            }
        }

        [HttpGet]
        [Route("Consultar_Acuerdo")]

        public List<Acuerdos> Consultar_Acuerdos(int id_fraccionamiento)
        {
            List<Acuerdos> Lista_acuerdos = new List<Acuerdos>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM acuerdos WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;




                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        // DateTime now = DateTime.Now;
                        // DateTime Dateproximo_pago = DateTime.Now.AddDays(proximo_pago);

                        // string fechaActual = now.ToString("yyyy-MM-ddTHH:mm:ss");

                        DateTime ffecha = reader.GetDateTime(4);
                        //  string fecha = ffecha.ToString("yyyy-MM-dd");


                        Lista_acuerdos.Add(new Acuerdos() { id_acuerdo = reader.GetInt32(0), id_fraccionamiento = reader.GetInt32(1), asunto = reader.GetString(2), detalles = reader.GetString(3), fecha = ffecha.ToString("yyyy-MM-dd") });
                    }

                    // MessageBox.Show();



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

        [HttpPost]
        [Route("Cargar_Comprobante")]

        public string Guardar_archivo(IFormFile file)
        {
            int id_usuario = 1;
            string tipo_deuda = "Ordinaria", nombre_deuda = "Mantenimiento", ruta_comprobante = "";
            double monto = 540.215;

            bool archivo_guardado = false;

            if (file.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    byte[] archivoEnBytes = memoryStream.ToArray(); // Convertir a byte[]

                    // Aquí puedes usar 'archivoEnBytes' como necesites
                    Pagos obj_pagos = new Pagos();
                    if (obj_pagos.Cargar_Comprobante_pago(archivoEnBytes))
                    {
                        return "si jala";
                    }
                    else
                    {
                        return "no jala";
                    }

                }
            }
            return "hola";
        }

        [HttpGet]
        [Route("Consultar_Comprobante")]

        public IActionResult Consultar_Comprobante(int id_Pago)
        {
            Pagos obj_pagos = new Pagos();

            byte[] imagenBytes = obj_pagos.ConsultarComprobantePago(id_Pago); // Lógica para obtener los bytes de la imagen desde tu base de datos

            // Devolver los bytes como contenido binario
            return File(imagenBytes, "image/jpeg"); // Cambia el tipo de contenido según el formato de tu imagen
        }



        [HttpGet]
        [Route("Consultar_Acuerdos_Paginados")]

        public List<Acuerdos> Consultar_Acuerdos_Paginados(int id_fraccionamiento, int indice, int rango)
        {


            List<Acuerdos> Lista_acuerdos = new List<Acuerdos>();




            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM acuerdos WHERE id_fraccionamiento=@id_fraccionamiento LIMIT @indice, @rango", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@indice", MySqlDbType.Int32).Value = indice;
                comando.Parameters.Add("@rango", MySqlDbType.Int32).Value = rango;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime ffecha = reader.GetDateTime(4);
                        //  string fecha = ffecha.ToString("yyyy-MM-dd");


                        Lista_acuerdos.Add(new Acuerdos() { id_acuerdo = reader.GetInt32(0), id_fraccionamiento = reader.GetInt32(1), asunto = reader.GetString(2), detalles = reader.GetString(3), fecha = ffecha.ToString("yyyy-MM-dd") });
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



        [HttpGet]
        [Route("Filtrar_Acuerdos_Fecha")]

        public List<Acuerdos> Filtrar_Acuerdos_Fecha(int id_fraccionamiento, int indice, int rango, string fechaOriginal)
        {


            List<Acuerdos> Lista_acuerdos = new List<Acuerdos>();

           // string fechaOriginal = "15-01-2024";

            // Convertir la fecha al formato deseado '15/01/2024'
            DateTime fecha = DateTime.ParseExact(fechaOriginal, "yyyy-MM-dd", null);
            string fechaFormateada = fecha.ToString("dd/MM/yyyy");


            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM acuerdos WHERE id_fraccionamiento=@id_fraccionamiento AND fecha = STR_TO_DATE(@fecha, '%d/%m/%Y') LIMIT @indice, @rango", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@indice", MySqlDbType.Int32).Value = indice;
                comando.Parameters.Add("@rango", MySqlDbType.Int32).Value = rango;
                comando.Parameters.Add("@fecha", MySqlDbType.VarChar).Value = fechaFormateada;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime ffecha = reader.GetDateTime(4);
                        //  string fecha = ffecha.ToString("yyyy-MM-dd");


                        Lista_acuerdos.Add(new Acuerdos() { id_acuerdo = reader.GetInt32(0), id_fraccionamiento = reader.GetInt32(1), asunto = reader.GetString(2), detalles = reader.GetString(3), fecha = ffecha.ToString("yyyy-MM-dd") });
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






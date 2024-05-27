using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Reflection.PortableExecutable;

namespace API_Archivo.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NotificacionesController : ControllerBase
    {

        [HttpPost]
        [Route("Agregar_Notificacion")]

        public bool Agregar_Notificacion(int id_fraccionamiento, string tipo, int id_destinatario, string asunto, string mensaje)
        {

            bool Notificacion_agregada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into notificaciones ( id_fraccionamiento, Tipo, id_destinatario, Asunto, Mensaje) VALUES (@id_fraccionamiento, @Tipo, @id_destinatario, @Asunto, @Mensaje)", conexion);

                //id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, Destinatario=@Destinatario, Asunto=@Asunto, Mensaje=@Mensaje

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = tipo;

                if (tipo == "General")
                {
                    comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = 0;
                }
                else
                {
                    comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = id_destinatario;
                }


                comando.Parameters.Add("@Asunto", MySqlDbType.VarChar).Value = asunto;
                comando.Parameters.Add("@Mensaje", MySqlDbType.VarChar).Value = mensaje;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Notificacion_agregada = true;
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
                return Notificacion_agregada;
            }
        }


        [HttpDelete]
        [Route("Eliminar_Notificacion")]


        public bool Eliminar_Notificacion(int id_notificacion)
        {

            bool Notificacion_eliminada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM notificaciones WHERE id_Notificacion=@id_Notificacion", conexion);

                //@id_usuario, @Tipo_deuda,@Nombre_deuda, @Monto, @Ruta_comprobante, @Estado

                comando.Parameters.Add("@id_Notificacion", MySqlDbType.Int32).Value = id_notificacion;



                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Notificacion_eliminada = true;
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
                return Notificacion_eliminada;

            }




        }


        [HttpPatch]
        [Route("Actualizar_Notificacion")]

        public bool Actualizar_Notificacion(int id_notificacion, int id_fraccionamiento, string tipo, string id_destinatario, string asunto, string mensaje)
        {
            bool Notificacion_actualizada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE notificaciones " +
                    "SET id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, id_destinatario=@id_destinatario, Asunto=@Asunto, Mensaje=@Mensaje " +
                    "WHERE id_Notificacion=@id_Notificacion", conexion);
                //id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, Destinatario=@Destinatario, Asunto=@Asunto, Mensaje=@Mensaje

                comando.Parameters.Add("@id_Notificacion", MySqlDbType.Int32).Value = id_notificacion;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@Tipo", MySqlDbType.VarChar).Value = tipo;

                if (tipo == "General")
                {
                    comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = 0;
                }
                else
                {
                    comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = id_destinatario;
                }

                comando.Parameters.Add("@Asunto", MySqlDbType.VarChar).Value = asunto;
                comando.Parameters.Add("@Mensaje", MySqlDbType.VarChar).Value = mensaje;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Notificacion_actualizada = true;
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
                return Notificacion_actualizada;
            }
        }


        [HttpGet]
        [Route("Consultar_Notificacion")]

        public List<Notificaciones> Consultar_Notificacion(int id_fraccionamiento, int id_destinatario, int indice, int rango)
        {
            /* Para consultar notificaciones se utilizará el id del fraccionamiento y el id del destinatario,
         * cuando se quieran consultar las notificaciones generales(de todos los usuarios) se deberá 
         * utilizar el numero cero (0) como parametro en el id del destinatario.
         * Cuando se quiera consultar las notificaciones especificas de un usuario se deberá utilizar su id
         * de usuario */

            List<Notificaciones> Lista_notificaciones = new List<Notificaciones>();

            

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM notificaciones WHERE id_fraccionamiento=@id_fraccionamiento && id_destinatario=@id_destinatario LIMIT @indice, @rango", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = id_destinatario;
                comando.Parameters.Add("@indice", MySqlDbType.Int32).Value = indice;
                comando.Parameters.Add("@rango", MySqlDbType.Int32).Value = rango;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_notificaciones.Add(new Notificaciones() { 
                            id_notificacion = reader.GetInt32(0), 
                            id_fraccionamiento = reader.GetInt32(1), 
                            tipo = reader.GetString(2), 
                            id_destinatario = reader.GetInt32(3), 
                            asunto = reader.GetString(4), 
                            mensaje = reader.GetString(5) 
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

                return Lista_notificaciones;
            }

        }



        [HttpGet]
        [Route("Consultar_Notificaciones")]

        public List<Notificaciones> Consultar_Notificaciones(int id_fraccionamiento, int indice, int rango, int id_destinatario)
        {
            /* Para consultar notificaciones se utilizará el id del fraccionamiento y el id del destinatario,
         * cuando se quieran consultar las notificaciones generales(de todos los usuarios) se deberá 
         * utilizar el numero cero (0) como parametro en el id del destinatario.
         * Cuando se quiera consultar las notificaciones especificas de un usuario se deberá utilizar su id
         * de usuario */

            List<Notificaciones> Lista_notificaciones = new List<Notificaciones>();

            string query = " ";

            if (id_destinatario == 2)
            {
                id_destinatario = 0;
                query = "OR id_destinatario=1";
            }

          //  int consulta = calcular_registros(id_fraccionamiento, "notificaciones");



            /*
            if (indice+rango > consulta)
            {
                indice = indice - rango;

            }


            if (indice < 0)
            {
                indice = 0;

            }
            */

           // Lista_notificaciones.Add(new Notificaciones() { id_notificacion = indice });


            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM notificaciones WHERE id_fraccionamiento=@id_fraccionamiento AND id_destinatario=@id_destinatario "+ query +" LIMIT @indice, @rango", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@indice", MySqlDbType.Int32).Value = indice;
                comando.Parameters.Add("@rango", MySqlDbType.Int32).Value = rango;
                comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = id_destinatario;

                //  comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = id_destinatario;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_notificaciones.Add(new Notificaciones() { id_notificacion = reader.GetInt32(0), id_fraccionamiento = reader.GetInt32(1), tipo = reader.GetString(2), id_destinatario = reader.GetInt32(3), asunto = reader.GetString(4), mensaje = reader.GetString(5) });
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

  

                return Lista_notificaciones;
            }

        } 


        [HttpGet]
        [Route("Buscar_Notificacion")]

        public List<Notificaciones> Buscar_Notificacion(int id_fraccionamiento, int id_destinatario)
        {
            /* Para consultar notificaciones se utilizará el id del fraccionamiento y el id del destinatario,
         * cuando se quieran consultar las notificaciones generales(de todos los usuarios) se deberá 
         * utilizar el numero cero (0) como parametro en el id del destinatario.
         * Cuando se quiera consultar las notificaciones especificas de un usuario se deberá utilizar su id
         * de usuario */

            List<Notificaciones> Lista_notificaciones = new List<Notificaciones>();



            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM notificaciones WHERE id_fraccionamiento=@id_fraccionamiento && id_destinatario=@id_destinatario", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@id_destinatario", MySqlDbType.Int32).Value = id_destinatario;
               // comando.Parameters.Add("@indice", MySqlDbType.Int32).Value = indice;
              //  comando.Parameters.Add("@rango", MySqlDbType.Int32).Value = rango;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_notificaciones.Add(new Notificaciones()
                        {
                            id_notificacion = reader.GetInt32(0),
                            id_fraccionamiento = reader.GetInt32(1),
                            tipo = reader.GetString(2),
                            id_destinatario = reader.GetInt32(3),
                            asunto = reader.GetString(4),
                            mensaje = reader.GetString(5)
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

                return Lista_notificaciones;
            }

        }



    }
}

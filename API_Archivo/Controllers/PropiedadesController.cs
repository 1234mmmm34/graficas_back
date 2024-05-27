using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropiedadesController : ControllerBase
    {
        public static string cadena_conexion = Global.cadena_conexion;

        [HttpPost]
        [Route("Agregar_Propiedad")]


        public bool Agregar_Propiedad([FromBody] Propiedades request)
        {
            bool Propiedad_agregada = false;


            using (MySqlConnection conexion = new MySqlConnection(cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into lotes (id_lote, id_fraccionamiento, id_propietario, tipo, descripcion, direccion, id_renta, id_administrador) VALUES (@id_lote,@id_fraccionamiento, @id_propietario, @tipo, @descripcion, @direccion, @id_renta, @id_administrador)", conexion);

                //@id_usuario, @Tipo_deuda,@Nombre_deuda, @Monto, @Ruta_comprobante, @Estado

                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_lote;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                comando.Parameters.Add("@id_propietario", MySqlDbType.Int32).Value = request.id_propietario;
                comando.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = request.tipo;
                comando.Parameters.Add("@descripcion", MySqlDbType.VarChar).Value = request.descripcion;
                comando.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = request.direccion;
                comando.Parameters.Add("@id_renta", MySqlDbType.Int32).Value = request.id_renta;
                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = request.id_administrador;



                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Propiedad_agregada = true;
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
                return Propiedad_agregada;

            }
        }

        

        [HttpDelete]
        [Route("Eliminar_Propiedad")]

        public bool Eliminar_propiedad(int id_lote)
        {
            bool Propiedad_eliminada = false;


            using (MySqlConnection conexion = new MySqlConnection(cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM lotes WHERE id_lote = @id_lote", conexion);

                //@id_usuario, @Tipo_deuda,@Nombre_deuda, @Monto, @Ruta_comprobante, @Estado

                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = id_lote;
              //  comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Propiedad_eliminada = true;
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
                return Propiedad_eliminada;

            }


        }
        
        [HttpPut]
        [Route("Actualizar_Propiedad")]


        public bool Actualizar_Propiedad([FromBody] Propiedades request)
        {
            bool Propiedad_actualizada = false;

            using (MySqlConnection conexion = new MySqlConnection(cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE lotes " +
                    "SET id_fraccionamiento=@id_fraccionamiento, " +
                    "descripcion=@descripcion, " +
                    "tipo=@tipo, " +
                    "direccion=@direccion, " +
                    "id_propietario=@id_propietario, " +
                    "id_renta=@id_renta " +
                    "WHERE id_lote=@id_lote", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                comando.Parameters.Add("@descripcion", MySqlDbType.VarChar).Value = request.descripcion;
                comando.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = request.tipo;
                comando.Parameters.Add("@direccion", MySqlDbType.VarChar).Value = request.direccion;
                comando.Parameters.Add("@id_propietario", MySqlDbType.Int32).Value = request.id_propietario;
                comando.Parameters.Add("@id_renta", MySqlDbType.Int32).Value = request.id_renta;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_lote;

                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Propiedad_actualizada = true;
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
                return Propiedad_actualizada;

            }


        }



        [HttpGet]
        [Route("Consultar_Propiedades")]

        public List<Propiedades> Consultar_Propiedad(int id_administrador)
        {

            List<Propiedades> Propiedad = new List<Propiedades>();
            string nombre_renta="", nombre="";

            using (MySqlConnection conexion = new MySqlConnection(cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM lotes WHERE id_administrador=@id_administrador", conexion);

                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = id_administrador;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        
                    /*    if (reader.GetInt32(6) == 0)
                        {
                            nombre_renta = "N/A";
                        }
                        else { 
                    */
                            nombre_renta = consultar_nombre(reader.GetInt32(6)); 
                        /*
                        }

                        if (reader.GetInt32(5) == 0)
                        {
                            nombre = "Correo enviado";
                        }
                        else
                        {
                        */
                            nombre = consultar_nombre(reader.GetInt32(5));
                      //  }

                        Propiedad.Add(new Propiedades()
                        {
                            id_lote = reader.GetInt32(0),
                            id_fraccionamiento = reader.GetInt32(1),
                            descripcion = reader.GetString(2),
                            tipo = reader.GetString(3),
                            direccion = reader.GetString(4),
                            id_propietario = reader.GetInt32(5),
                            id_renta = reader.GetInt32(6),
                            nombre = nombre,
                            nombre_renta = nombre_renta

                        }) ;
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

                return Propiedad;
            }

        }

        [HttpGet]
        [Route("Consultar_nombre")]
        public string consultar_nombre(int id_persona)
        {
            string nombre="Correo enviado";

            using (MySqlConnection conexion1 = new MySqlConnection(cadena_conexion))
            {

                MySqlCommand comando1 = new MySqlCommand("SELECT * FROM personas WHERE id_persona=@id_persona", conexion1);

                comando1.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = id_persona;


                try
                {

                    conexion1.Open();

                    MySqlDataReader reader1 = comando1.ExecuteReader();

                    while (reader1.Read())
                    {
                        /*
                        Propiedad.Add(new Propiedades()
                        {
                            id_lote = reader.GetInt32(0),
                            id_fraccionamiento = reader.GetInt32(1),
                            descripcion = reader.GetString(2),
                            tipo = reader.GetString(3),
                            direccion = reader.GetString(4),
                            id_propietario = reader.GetInt32(5),
                            id_renta = reader.GetInt32(6)
                        });
                        */
                        nombre = reader1.GetString(1) + " " + reader1.GetString(2) + " " + reader1.GetString(3);
                        // MessageBox.Show();
                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion1.Close();
                }

                return nombre;
            }
        }

    }
}

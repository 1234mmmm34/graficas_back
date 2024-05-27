using API_Archivo.Clases;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EgresosController
    {
        [HttpPost]
        [Route("Agregar_Egreso")]

        public bool Agregar_Egreso(int id_fraccionamiento, string concepto, string descripcion, string proveedor, double monto, string fecha)
        {
            bool egreso_agregado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into egresos (id_fraccionamiento, concepto, descripcion, proveedor, monto, fecha) VALUES (@id_fraccionamiento, @concepto, @descripcion, @proveedor, @monto, @fecha)", conexion);

                //id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, Destinatario=@Destinatario, Asunto=@Asunto, Mensaje=@Mensaje

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@concepto", MySqlDbType.VarChar).Value = concepto;
                comando.Parameters.Add("@descripcion", MySqlDbType.VarChar).Value = descripcion;
                comando.Parameters.Add("@proveedor", MySqlDbType.VarChar).Value = proveedor;
                comando.Parameters.Add("@monto", MySqlDbType.Double).Value = monto;
                comando.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        egreso_agregado = true;
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
                return egreso_agregado;
            }

        }

        [HttpDelete]
        [Route("Eliminar_Egreso")]

        public bool Eliminar_Egreso(int id_egreso, int id_fraccionamiento)
        {
            bool egreso_eliminado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM egresos WHERE id_egreso=@id_egreso && id_fraccionamiento=@id_fraccionamiento", conexion);

                //@id_usuario, @Tipo_deuda,@Nombre_deuda, @Monto, @Ruta_comprobante, @Estado

                comando.Parameters.Add("@id_egreso", MySqlDbType.Int32).Value = id_egreso;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        egreso_eliminado = true;
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
                return egreso_eliminado;

            }
        }

        [HttpPatch]
        [Route("Actualizar_Egreso")]


        public bool Actualizar_Egreso(int id_egreso, int id_fraccionamiento, string concepto, string descripcion, string proveedor, double monto, string fecha)
        {
            bool egreso_actualizado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE egresos " +
                    "SET concepto=@concepto, descripcion=@descripcion, proveedor=@proveedor, monto=@monto, fecha=@fecha " +
                    "WHERE id_egreso=@id_egreso && id_fraccionamiento=@id_fraccionamiento", conexion);
                //id_fraccionamiento=@id_fraccionamiento, Tipo=@Tipo, Destinatario=@Destinatario, Asunto=@Asunto, Mensaje=@Mensaje

                comando.Parameters.Add("@concepto", MySqlDbType.VarChar).Value = concepto;
                comando.Parameters.Add("@descripcion", MySqlDbType.VarChar).Value = descripcion;
                comando.Parameters.Add("@proveedor", MySqlDbType.VarChar).Value = proveedor;
                comando.Parameters.Add("@monto", MySqlDbType.VarChar).Value = monto;
                comando.Parameters.Add("@fecha", MySqlDbType.Date).Value = fecha;

                comando.Parameters.Add("@id_egreso", MySqlDbType.Int32).Value = id_egreso;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        egreso_actualizado = true;
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
                return egreso_actualizado;
            }

        }


        [HttpGet]
        [Route("Consultar_Egreso")]

        public List<Egresos> Consultar_Egresos(int id_fraccionamiento)
        {
            List<Egresos> Lista_egresos = new List<Egresos>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM egresos WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_egresos.Add(new Egresos() { id_egreso = reader.GetInt32(0), concepto = reader.GetString(2), descripcion = reader.GetString(3), proveedor = reader.GetString(4), monto = reader.GetDouble(5), fecha = reader.GetString(6) });

                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return Lista_egresos;
            }


        }

        [HttpGet]
        [Route("Consultar_Egreso_especifico")]

        public List<Egresos> Consultar_Egreso_Especifico(int id_egreso, int id_fraccionamiento)
        {
            List<Egresos> Lista_egresos = new List<Egresos>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM egresos WHERE id_fraccionamiento=@id_fraccionamiento && id_Egreso=@id_egreso", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@id_egreso", MySqlDbType.Int32).Value = id_egreso;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_egresos.Add(new Egresos() { concepto = reader.GetString(2), descripcion = reader.GetString(3), proveedor = reader.GetString(4), monto = reader.GetDouble(5), fecha = reader.GetString(6) });
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

                return Lista_egresos;
            }


        }

    }
}

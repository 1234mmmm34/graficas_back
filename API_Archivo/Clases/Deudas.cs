

using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Reflection.PortableExecutable;

namespace API_Archivo.Clases
{
    public class Deudas

    {
        public string destinatario { get; set; }
        public int id_deudas { get; set; } //primary key

        public int id_fraccionamiento { get; set; }

        public int id_tesorero { get; set; }

        public float monto { get; set; }

        public string nombre { get; set; }

        public string descripcion { get; set; }

        public int dias_gracia { get; set; }

        public int periodicidad { get; set; }

        public float recargo { get; set; }

        public DateTime proximo_pago { get; set; }

        public string proximo_pago1 { get; set; }

        public int deudor { get; set; }


        [HttpGet]
        [Route("Consultar_DeudasOrdinarias")]

        public List<Deudas> Consultar_DeudasOrdinarias(int id_tesorero)
        {

            List<Deudas> Deuda = new List<Deudas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM deudas WHERE id_fraccionamiento=@id_fraccionamiento && periodicidad>0", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_tesorero;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Deuda.Add(new Deudas()
                        {
                            id_deudas = reader.GetInt32(0),
                            monto = reader.GetFloat(3),
                            nombre = reader.GetString(4),
                            descripcion = reader.GetString(5),
                            dias_gracia = reader.GetInt32(6),
                            periodicidad = reader.GetInt32(7),
                            recargo = reader.GetFloat(8),
                            proximo_pago = reader.GetDateTime(9)

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

                return Deuda;
            }

        }


        [HttpGet]
        [Route("Consultar_DeudasExtraordinarias")]

        public List<Deudas> Consultar_DeudasExtraordinarias(int id_tesorero)
        {

            List<Deudas> Deuda = new List<Deudas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM deudas WHERE id_tesorero=@id_tesorero && periodicidad=0", conexion);

                comando.Parameters.Add("@id_tesorero", MySqlDbType.Int32).Value = id_tesorero;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Deuda.Add(new Deudas()
                        {
                            id_deudas = reader.GetInt32(0),
                            monto = reader.GetFloat(3),
                            nombre = reader.GetString(4),
                            descripcion = reader.GetString(5),
                            dias_gracia = reader.GetInt32(6),
                            periodicidad = reader.GetInt32(7),
                            recargo = reader.GetFloat(8),
                            proximo_pago = reader.GetDateTime(9)

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

                return Deuda;
            }

        }


        [HttpGet]
        [Route("Consultar_DeudaPorId")]

        public List<Deudoress> Consultar_DeudaPorId(int id_deudor, int id_deuda, int id_fraccionamiento)
        {

            List<Deudoress> Deuda = new List<Deudoress>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM deudores WHERE id_deudor=@id_deudor && id_deuda=@id_deuda && id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_deudor", MySqlDbType.Int64).Value = id_deudor;
                comando.Parameters.Add("@id_deuda", MySqlDbType.Int64).Value = id_deuda;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int64).Value = id_fraccionamiento;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Deuda.Add(new Deudoress()
                        {
                            id_deudor = reader.GetInt32(1),
                            id_deuda = reader.GetInt32(2),
                            id_fraccionamiento = reader.GetInt32(3),
                            nombre_persona = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                            lote = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                            tipo_deuda = reader.GetString(6),
                            nombre_deuda = reader.GetString(7),
                            monto = reader.GetFloat(8),
                            recargo = reader.GetFloat(9),
                            dias_gracia = reader.GetInt32(10),
                            proximo_pago = (reader.GetDateTime(11)).ToString("yyyy-MM-ddTHH:mm:ss"),
                            estado = reader.GetString(12),
                            periodicidad = reader.GetInt32(13)

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

                return Deuda;
            }
        }

        [HttpGet]
        [Route("AsignarDeudaNuevaATodos")]
        public bool AsignarDeudaNuevaATodos(int id_fraccionamiento, string destinatario)
        {
            List<Deudas> Deuda = new List<Deudas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("select * from deudas where id_fraccionamiento=@id_fraccionamiento order by id_deudas desc limit 1", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Deuda.Add(new Deudas()
                        {
                            id_deudas = reader.GetInt32(0),
                            monto = reader.GetFloat(3),
                            nombre = reader.GetString(4),
                            descripcion = reader.GetString(5),
                            dias_gracia = reader.GetInt32(6),
                            periodicidad = reader.GetInt32(7),
                            recargo = reader.GetFloat(8),
                            proximo_pago = reader.GetDateTime(9)

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


            }

            List<Personas> Persona = new List<Personas>();
            bool isindividual = false;
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                string query = "";


                if (destinatario == "todos")
                {
                    query = "SELECT * FROM personas where (id_administrador=@id_administrador)";
                }
                else
                {
                    isindividual = true;
                    query = "SELECT * FROM personas where id_administrador=@id_administrador && id_persona=@id_persona";
                }
                //query = "SELECT * FROM personas where (id_administrador=@id_administrador) && (tipo_usuario='arrendatario' OR tipo_usuario='propietario' OR tipo_usuario='usuario')";


                MySqlCommand comando = new MySqlCommand(query, conexion);
                //y id_fraccionamiento
                //  comando.Parameters.Add("@Nombre", MySqlDbType.Int32).Value = nombre;
                //  comando.Parameters.Add("@Apellido_pat", MySqlDbType.Int32).Value = apellido_pat;
                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = id_fraccionamiento;
                if (isindividual)
                {
                    comando.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = Convert.ToInt32(destinatario);
                }



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Persona.Add(new Personas()
                        {
                            id_persona = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                            nombre = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            apellido_pat = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            apellido_mat = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            id_lote = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                            telefono = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            fecha_nacimiento = reader.IsDBNull(5) ? "0000-00-00" : Convert.ToString(reader.GetDateTime(5)),
                            correo = reader.IsDBNull(10) ? "" : reader.GetString(10),
                            contrasenia = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            id_fraccionamiento = reader.IsDBNull(6) ? -1 : reader.GetInt32(6),
                            tipo_usuario = reader.IsDBNull(13) ? "" : reader.GetString(13),
                            intercomunicador = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8),
                            codigo_acceso = reader.IsDBNull(9) ? "" : reader.GetString(9)
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


            }


            //asignar la deuda a todas las personas

            bool resultado = false;
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                conexion.Open();

                foreach (Personas persona in Persona)
                {
                    MySqlCommand insertDeudor = new MySqlCommand(
                        "INSERT INTO deudores (id_deudor,id_deuda, id_fraccionamiento,nombre_persona, lote, tipo_deuda, nombre_deuda, monto,recargo,dias_gracia, proximo_pago, estado,periodicidad) " +
                        "VALUES (@id_deudor,@id_deuda, @id_fraccionamiento,@nombre_persona, @lote, @tipo_deuda, @nombre_deuda, @monto,@recargo,@dias_gracia, @proximo_pago, @estado,@periodicidad)", conexion);

                    insertDeudor.Parameters.AddWithValue("@id_deudor", persona.id_persona);
                    insertDeudor.Parameters.AddWithValue("@id_fraccionamiento", persona.id_fraccionamiento);
                    insertDeudor.Parameters.AddWithValue("@nombre_persona", persona.nombre + " " + persona.apellido_pat + " " + persona.apellido_mat);
                    insertDeudor.Parameters.AddWithValue("@lote", persona.id_lote);
                    if (Deuda[0].periodicidad > 0)
                    {
                        insertDeudor.Parameters.AddWithValue("@tipo_deuda", "ordinaria");
                    }
                    else
                    {
                        insertDeudor.Parameters.AddWithValue("@tipo_deuda", "extraordinaria");

                    }
                    insertDeudor.Parameters.AddWithValue("@nombre_deuda", Deuda[0].nombre);
                    insertDeudor.Parameters.AddWithValue("@monto", Deuda[0].monto);
                    insertDeudor.Parameters.AddWithValue("@proximo_pago", Deuda[0].proximo_pago);
                    insertDeudor.Parameters.AddWithValue("@estado", "Pendiente");
                    insertDeudor.Parameters.AddWithValue("@periodicidad", Deuda[0].periodicidad);
                    insertDeudor.Parameters.AddWithValue("@id_deuda", Deuda[0].id_deudas);
                    insertDeudor.Parameters.AddWithValue("@recargo", Deuda[0].recargo);
                    insertDeudor.Parameters.AddWithValue("@dias_gracia", Deuda[0].dias_gracia);

                    try
                    {
                        int rowsaffected = insertDeudor.ExecuteNonQuery();
                        if (rowsaffected >= 1)
                        {
                            resultado = true;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        // Manejo de excepciones
                    }
                }

                conexion.Close();
            }
            return resultado;

        }

        [HttpGet]
        [Route("AsignarDeudasAUltimaPersona")]

        public bool AsignarDeudasAUltimaPersona(int id_fraccionamiento)
        {
            List<Personas> UltimaPersona = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                MySqlCommand comandoUltimaPersona = new MySqlCommand("SELECT * FROM personas ORDER BY id_persona DESC LIMIT 1", conexion);

                try
                {
                    conexion.Open();

                    MySqlDataReader reader = comandoUltimaPersona.ExecuteReader();

                    while (reader.Read())
                    {
                        UltimaPersona.Add(new Personas()
                        {
                            id_persona = reader.IsDBNull(0) ? -1 : reader.GetInt32(0),
                            nombre = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            apellido_pat = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            apellido_mat = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            id_lote = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                            telefono = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            fecha_nacimiento = reader.IsDBNull(5) ? "0000-00-00" : Convert.ToString(reader.GetDateTime(5)),
                            correo = reader.IsDBNull(10) ? "" : reader.GetString(10),
                            contrasenia = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            id_fraccionamiento = reader.IsDBNull(6) ? -1 : reader.GetInt32(6),
                            tipo_usuario = reader.IsDBNull(13) ? "" : reader.GetString(13),
                            intercomunicador = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8),
                            codigo_acceso = reader.IsDBNull(9) ? "" : reader.GetString(9)
                        });
                    }

                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    // Manejo de excepciones
                }
                finally
                {
                    conexion.Close();
                }
            }

            List<Deudas> TodasLasDeudas = new List<Deudas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                MySqlCommand comandoTodasLasDeudas = new MySqlCommand("SELECT * FROM deudas", conexion);

                try
                {
                    conexion.Open();

                    MySqlDataReader reader = comandoTodasLasDeudas.ExecuteReader();

                    while (reader.Read())
                    {
                        TodasLasDeudas.Add(new Deudas()
                        {
                            id_deudas = reader.GetInt32(0),
                            monto = reader.GetFloat(3),
                            nombre = reader.GetString(4),
                            descripcion = reader.GetString(5),
                            dias_gracia = reader.GetInt32(6),
                            periodicidad = reader.GetInt32(7),
                            recargo = reader.GetFloat(8),
                            proximo_pago = reader.GetDateTime(9)

                        });
                    }

                    reader.Close();
                }
                catch (MySqlException ex)
                {
                    // Manejo de excepciones
                }
                finally
                {
                    conexion.Close();
                }
            }

            bool resultado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                try
                {
                    conexion.Open();

                    foreach (Deudas deuda in TodasLasDeudas)
                    {
                        MySqlCommand insertDeudor = new MySqlCommand(
                            "INSERT INTO deudores (id_deudor, id_deuda, id_fraccionamiento, lote, tipo_deuda, nombre_deuda, monto, proximo_pago, estado, periodicidad) " +
                            "VALUES (@id_deudor, @id_deuda, @id_fraccionamiento, @lote, @tipo_deuda, @nombre_deuda, @monto, @proximo_pago, @estado, @periodicidad)", conexion);

                        insertDeudor.Parameters.AddWithValue("@id_deudor", UltimaPersona[0].id_persona);
                        insertDeudor.Parameters.AddWithValue("@id_fraccionamiento", UltimaPersona[0].id_fraccionamiento);
                        insertDeudor.Parameters.AddWithValue("@lote", UltimaPersona[0].id_lote);

                        if (deuda.periodicidad > 0)
                        {
                            insertDeudor.Parameters.AddWithValue("@tipo_deuda", "ordinaria");
                        }
                        else
                        {
                            insertDeudor.Parameters.AddWithValue("@tipo_deuda", "extraordinaria");
                        }

                        insertDeudor.Parameters.AddWithValue("@nombre_deuda", deuda.nombre);
                        insertDeudor.Parameters.AddWithValue("@monto", deuda.monto);
                        insertDeudor.Parameters.AddWithValue("@proximo_pago", deuda.proximo_pago);
                        insertDeudor.Parameters.AddWithValue("@estado", "Pendiente");
                        insertDeudor.Parameters.AddWithValue("@periodicidad", deuda.periodicidad);
                        insertDeudor.Parameters.AddWithValue("@id_deuda", deuda.id_deudas);

                        int rowsAffected = insertDeudor.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            resultado = true;
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Manejo de excepciones
                    resultado = false;
                }
                finally
                {
                    conexion.Close();
                }
            }

            return resultado;

        }//fin del metodo

        [HttpDelete]
        [Route("EliminarDeudasAUsuarios")]
        public bool EliminarDeudasAUsuarios(int id_deuda)
        {
            bool Deuda_eliminada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM deudores WHERE id_deuda=@id_deuda", conexion);

                comando.Parameters.Add("@id_deuda", MySqlDbType.Int32).Value = id_deuda;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Deuda_eliminada = true;
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
                return Deuda_eliminada;

            }
        }

        [HttpGet]
        [Route("Consultar_Comprobante")]

        public byte[] Consultar_Comprobante(int id)
        {
            byte[] imagen = null;
            string connectionString = Global.cadena_conexion;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT comprobante FROM historial_deudaspagadas WHERE id=@id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal("comprobante")))
                                {
                                    long size = reader.GetBytes(reader.GetOrdinal("comprobante"), 0, null, 0, 0);
                                    imagen = new byte[size];
                                    reader.GetBytes(reader.GetOrdinal("comprobante"), 0, imagen, 0, (int)size);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar la imagen en la base de datos: " + ex.Message);
            }

            return imagen;
        }
    }
}
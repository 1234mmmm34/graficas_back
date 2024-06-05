using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;

namespace API_Archivo.Clases
{
    public class Entradas
    {
        public int id_entrada { get; set; }

        public DateTime fecha { get; set; }

        public string nombre { get; set; }

        public List<Entradas> Consultar_Entradas()
        {
            List<Entradas> Lista_entradas = new List<Entradas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM entradas", conexion);

                //comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;

                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {

                        Lista_entradas.Add(new Entradas() { id_entrada = reader.GetInt32(0), fecha = reader.GetDateTime(1), nombre = reader.GetString(2) });
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

                return Lista_entradas;
            }
        }

        public List<Entradas> Consultar_EntradasPorPersona(string nombre_persona)
        {
            List<Entradas> Lista_entradas = new List<Entradas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM entradas WHERE nombre=@nombre", conexion);

                comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre_persona;

                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {

                        Lista_entradas.Add(new Entradas() { id_entrada = reader.GetInt32(0), fecha = reader.GetDateTime(1), nombre = reader.GetString(2) });
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

                return Lista_entradas;
            }
        }

        public List<Entradas> Consultar_EntradasPorFecha(string fecha_inicio, string fecha_final)
        {
            List<Entradas> Lista_entradas = new List<Entradas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM entradas WHERE fecha>=@fecha_inicio and fecha<=@fecha_final;", conexion);

                comando.Parameters.Add("@fecha_inicio", MySqlDbType.VarChar).Value = fecha_inicio;
                comando.Parameters.Add("@fecha_final", MySqlDbType.VarChar).Value = fecha_final; ;

                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {

                        Lista_entradas.Add(new Entradas() { id_entrada = reader.GetInt32(0), fecha = reader.GetDateTime(1), nombre = reader.GetString(2) });
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

                return Lista_entradas;
            }
        }


    }
}
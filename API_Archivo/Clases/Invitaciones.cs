using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    public class Invitaciones
    {


        public int id_invitacion { get; set; }
        public string token { get; set; }
        public string correo_electronico { get; set; }
        public int id_fraccionamiento { get; set; }
        public int lote { get; set; }
        public string nombre_fraccionamiento { get; set; }
        public string nombre_admin { get; set; }
        public string tipo_usuario { get; set; }

        public bool Generar_invitacion(string token, string correo_invitado, int id_fraccionamiento, int id_lote, string nombre_fraccionamiento, string nombre_admin, string tipo_usuario)
        {
            bool invitacion_agregada = false;
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into invitaciones (token, correo_invitado, id_fraccionamiento, lote, nombre_fraccionamiento,nombre_administrador,tipo_usuario) VALUES (@token, @correo_invitado, @id_fraccionamiento, @lote, @nombre_fraccionamiento,@nombre_administrador,@tipo_usuario)", conexion);



                comando.Parameters.Add("@token", MySqlDbType.VarChar).Value = token;
                comando.Parameters.Add("@correo_invitado", MySqlDbType.VarChar).Value = correo_invitado;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.VarChar).Value = id_fraccionamiento;
                comando.Parameters.Add("@lote", MySqlDbType.Int32).Value = id_lote;
                comando.Parameters.Add("@nombre_fraccionamiento", MySqlDbType.VarChar).Value = nombre_fraccionamiento;
                comando.Parameters.Add("@nombre_administrador", MySqlDbType.VarChar).Value = nombre_admin;
                comando.Parameters.Add("@tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;


                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        invitacion_agregada = true;
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
                return invitacion_agregada;
            }
        }//


        public List<Invitaciones> Cosultar_invitacion(string token)
        {
            List<Invitaciones> Lista_invitacion = new List<Invitaciones>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM invitaciones WHERE token=@token", conexion);

                comando.Parameters.Add("@token", MySqlDbType.VarChar).Value = token;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_invitacion.Add(new Invitaciones() { id_invitacion = reader.GetInt32(0), token = reader.GetString(1), correo_electronico = reader.GetString(2), id_fraccionamiento = reader.GetInt32(3), lote = reader.GetInt32(4), nombre_fraccionamiento = reader.GetString(5), nombre_admin = reader.GetString(6), tipo_usuario = reader.GetString(7) });
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

                return Lista_invitacion;
            }
        }
    }
}
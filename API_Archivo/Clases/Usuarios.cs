using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    
    public class Usuarios
    {
        /*
        public string? nombre { get; set; }
        public string? apellido_pat { get; set; }
        public string? apellido_mat { get; set; }
        public int? id_lote { get; set; }
        public string? telefono { get; set; }
        public DateOnly fecha_nacimiento { get; set; }
        public string? correo { get; set; }
        public string? contrasenia { get; set; }
        public int? id_fraccionamiento { get; set; }

        public int? id_administrador { get; set; }

        public string? tipo_usuario { get; set; }
    */

        public bool Cargar_Imagen(byte[] imagen_recibida, int id_persona)
        {
            bool resultado = false;
            string connectionString = Global.cadena_conexion;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE personas " +
                    "SET Imagen=@imagen " +
                    "WHERE id_Persona=@id_Persona";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@imagen", imagen_recibida);
                        command.Parameters.AddWithValue("@id_Persona", id_persona);
                        int rowsaffected = command.ExecuteNonQuery();
                        if (rowsaffected > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar la imagen en la base de datos: " + ex.Message);
            }
            finally
            {

            }
            return resultado;

        }

        public byte[] Consultar_Imagen(int id_Persona)
        {
            byte[] imagen = null;
            string connectionString = Global.cadena_conexion;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Imagen FROM personas WHERE id_Persona = @id_Persona";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_Persona", id_Persona);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal("Imagen")))
                                {
                                    long size = reader.GetBytes(reader.GetOrdinal("Imagen"), 0, null, 0, 0);
                                    imagen = new byte[size];
                                    reader.GetBytes(reader.GetOrdinal("Imagen"), 0, imagen, 0, (int)size);
                                }
                            }
                        }
                    }

                    connection.Close();

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
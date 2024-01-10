/*using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    public class Personas
    {

        public int id_persona { get; set; }
        public string? nombre {  get; set; }
        public string? apellido_pat {  get; set; }
        public string? apellido_mat { get; set;}
        public string? telefono {  get; set; }

        public int? id_fraccionamiento {  get; set; }
        public int? id_lote {  get; set; }
        public int? intercomunicador {  get; set; }

        public string? codigo_acceso {get; set;}

        public string? fecha_nacimiento { get; set; }

        public string? correo { get; set; }
        public string? contrasenia { get; set; }

        public string? tipo_usuario { get; set; }

        public bool Actualizar_Contrasenia(string correo, string contrasenia)
        {

            bool Persona_actualizada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE personas " +
                    "SET Contrasenia=@Contrasenia " +
                    "WHERE Correo=@Correo", conexion);

                comando.Parameters.Add("@Correo", MySqlDbType.VarChar).Value = correo;
                comando.Parameters.Add("@Contrasenia", MySqlDbType.VarChar).Value = contrasenia;


                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Persona_actualizada = true;
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
                return Persona_actualizada;

            }
        }



        public List<Personas> Consultar_Persona(string nombre, string apellido_pat, string apellido_mat)
        {
            List<Personas> Persona = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE Nombre=@Nombre && Apellido_pat=@Apellido_pat && Apellido_mat=@Apellido_mat", conexion);

                comando.Parameters.Add("@Nombre", MySqlDbType.Int32).Value = nombre;
                comando.Parameters.Add("@Apellido_pat", MySqlDbType.Int32).Value = apellido_pat;
                comando.Parameters.Add("@Apellido_mat", MySqlDbType.Int32).Value = apellido_mat;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Persona.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1), apellido_pat = reader.GetString(2), apellido_mat = reader.GetString(3), telefono = reader.GetString(4), tipo_usuario = reader.GetString(6), id_fraccionamiento = reader.GetInt32(7), id_lote = reader.GetInt32(8), intercomunicador = reader.GetInt32(9), codigo_acceso = reader.GetString(10), correo = reader.GetString(11) });
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

                return Persona;
            }
        }

        public List<Personas> Consultar_Personas_Por_Fraccionamiento(int id_fraccionamiento)
        {
            List<Personas> Lista_Personas = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_Personas.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1), apellido_pat = reader.GetString(2), apellido_mat = reader.GetString(3), telefono = reader.GetString(4), tipo_usuario = reader.GetString(6), id_fraccionamiento = reader.GetInt32(7), id_lote = reader.GetInt32(8), intercomunicador = reader.GetInt32(9), codigo_acceso = reader.GetString(10), correo = reader.GetString(11) });
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

                return Lista_Personas;
            }
        }

        public string Obtener_Correo_Persona(int id_persona)
        {
            string correo = "";
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT Correo FROM personas WHERE id_Persona=@id_Persona", conexion);

                comando.Parameters.Add("@id_Persona", MySqlDbType.Int32).Value = id_persona;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        correo = reader.GetString(0);
                    }
                    else
                    {
                        correo = "";
                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return correo;
            }
        }



        public List<Personas> Consultar_ArrendatariosYPropietarios(int id_fraccionamiento)
        {
            List<Personas> Lista_Personas = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE (id_fraccionamiento=@id_fraccionamiento) && (tipo_usuario='arrendatario' OR tipo_usuario='propietario')", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_Personas.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1) });
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

                return Lista_Personas;
            }
        }


    }

}
*/



using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    public class Personas
    {

        public int id_persona { get; set; }
        public string? nombre { get; set; }
        public string? apellido_pat { get; set; }
        public string? apellido_mat { get; set; }
        public string? telefono { get; set; }

        public int? id_fraccionamiento { get; set; }
        public int? id_lote { get; set; }
        public int? intercomunicador { get; set; }

        public string? codigo_acceso { get; set; }

        public string? fecha_nacimiento { get; set; }

        public string? correo { get; set; }
        public string? contrasenia { get; set; }

        public string? tipo_usuario { get; set; }

        public bool Actualizar_Contrasenia(string correo, string contrasenia)
        {

            bool Persona_actualizada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE personas " +
                    "SET Contrasenia=@Contrasenia " +
                    "WHERE Correo=@Correo", conexion);

                comando.Parameters.Add("@Correo", MySqlDbType.VarChar).Value = correo;
                comando.Parameters.Add("@Contrasenia", MySqlDbType.VarChar).Value = contrasenia;


                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Persona_actualizada = true;
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
                return Persona_actualizada;

            }
        }



        public List<Personas> Consultar_Persona(string nombre, string apellido_pat, string apellido_mat)
        {
            List<Personas> Persona = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE Nombre=@Nombre && Apellido_pat=@Apellido_pat && Apellido_mat=@Apellido_mat", conexion);

                comando.Parameters.Add("@Nombre", MySqlDbType.Int32).Value = nombre;
                comando.Parameters.Add("@Apellido_pat", MySqlDbType.Int32).Value = apellido_pat;
                comando.Parameters.Add("@Apellido_mat", MySqlDbType.Int32).Value = apellido_mat;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Persona.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1), apellido_pat = reader.GetString(2), apellido_mat = reader.GetString(3), telefono = reader.GetString(4), tipo_usuario = reader.GetString(6), id_fraccionamiento = reader.GetInt32(7), id_lote = reader.GetInt32(8), intercomunicador = reader.GetInt32(9), codigo_acceso = reader.GetString(10), correo = reader.GetString(11) });
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

                return Persona;
            }
        }

        public List<Personas> Consultar_Personas_Por_Fraccionamiento(int id_fraccionamiento)
        {
            List<Personas> Lista_Personas = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_Personas.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1) });
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

                return Lista_Personas;
            }
        }

        public string Obtener_Correo_Persona(int id_persona)
        {
            string correo = "";
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT Correo FROM personas WHERE id_Persona=@id_Persona", conexion);

                comando.Parameters.Add("@id_Persona", MySqlDbType.Int32).Value = id_persona;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        correo = reader.GetString(0);
                    }
                    else
                    {
                        correo = "";
                    }


                }
                catch (MySqlException ex)
                {

                }
                finally
                {
                    conexion.Close();
                }

                return correo;
            }
        }

        public List<Personas> Consultar_ArrendatariosYPropietarios(int id_fraccionamiento)
        {
            List<Personas> Lista_Personas = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE (id_fraccionamiento=@id_fraccionamiento) && (tipo_usuario='arrendatario' OR tipo_usuario='propietario')", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_Personas.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1) });
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

                return Lista_Personas;
            }
        }

    }

}
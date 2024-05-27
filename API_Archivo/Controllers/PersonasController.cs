using API_Archivo.Clases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace API_Archivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {

        [HttpPost]
        [Route("Agregar_Persona")]

        public bool Agregar_Persona(string nombre, string apellido_pat, string apellido_mat, string telefono, string fecha_nacimiento, int id_fraccionamiento, int id_lote, string intercomunicador, string codigo_acceso, string tipo_usuario, string hikvision)
        {
            bool Persona_agregada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into personas (Nombre, Apellido_pat, Apellido_mat, Telefono, Fecha_nacimiento,  id_fraccionamiento, id_lote, Intercomunicador, Codigo_acceso, Tipo_usuario, hikvision) VALUES ( @Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @id_fraccionamiento, @id_lote, @Intercomunicador, @Codigo_acceso, @tipo_usuario, @hikvision)", conexion);

                //@Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @Tipo_usuario, @id_fraccionamiento, @id_lote, @Intercomunicador, @Codigo_acceso

                comando.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = nombre;
                comando.Parameters.Add("@Apellido_pat", MySqlDbType.VarChar).Value = apellido_pat;
                comando.Parameters.Add("@Apellido_mat", MySqlDbType.VarChar).Value = apellido_mat;
                comando.Parameters.Add("@Telefono", MySqlDbType.VarChar).Value = telefono;
                comando.Parameters.Add("@Fecha_nacimiento", MySqlDbType.Date).Value = fecha_nacimiento;
                //    comando.Parameters.Add("@Tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value =  id_lote;
                comando.Parameters.Add("@Intercomunicador", MySqlDbType.VarChar).Value = intercomunicador;
                comando.Parameters.Add("@Codigo_acceso", MySqlDbType.VarChar).Value = codigo_acceso;
                comando.Parameters.Add("@tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;
                comando.Parameters.Add("@hikvision", MySqlDbType.VarChar).Value = hikvision;





                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Persona_agregada = true;
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
                return Persona_agregada;

            }
        }

        [HttpDelete]
        [Route("Eliminar_Persona")]
        public bool Eliminar_Persona(int id_persona)
        {
            bool Persona_eliminada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("DELETE FROM personas WHERE id_Persona=@id_persona", conexion);

                //@Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @Tipo_usuario, @id_fraccionamiento, @id_lote, @Intercomunicador, @Codigo_acceso

                // comando.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = nombre;
                //  comando.Parameters.Add("@Apellido_pat", MySqlDbType.VarChar).Value = apellido_paterno;
                comando.Parameters.Add("@id_Persona", MySqlDbType.Int32).Value = id_persona;




                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Persona_eliminada = true;
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
                return Persona_eliminada;

            }
        }

        [HttpPut]
        [Route("Actualizar_Persona")]
        public bool Actualizar_Persona([FromBody] Personas request)
        {

            bool Persona_actualizada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE personas " +
                    "SET nombre=@nombre, apellido_pat=@apellido_pat,apellido_mat=@apellido_mat, telefono=@telefono, fecha_nacimiento=@fecha_nacimiento, id_lote=@id_lote, intercomunicador=@intercomunicador, codigo_acceso=@codigo_acceso, tipo_usuario=@tipo_usuario, contrasenia = @contrasenia " +
                    "WHERE id_persona=@id_persona", conexion);

                comando.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = request.id_persona;
                comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = request.nombre;
                comando.Parameters.Add("@apellido_pat", MySqlDbType.VarChar).Value = request.apellido_pat;
                comando.Parameters.Add("@apellido_mat", MySqlDbType.VarChar).Value = request.apellido_mat;
                comando.Parameters.Add("@telefono", MySqlDbType.VarChar).Value = request.telefono;
                comando.Parameters.Add("@fecha_nacimiento", MySqlDbType.Date).Value = request.fecha_nacimiento1;
                //    comando.Parameters.Add("@Tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_lote;
                comando.Parameters.Add("@intercomunicador", MySqlDbType.Int32).Value = request.intercomunicador;
                comando.Parameters.Add("@codigo_acceso", MySqlDbType.VarChar).Value = request.codigo_acceso;
                comando.Parameters.Add("@tipo_usuario", MySqlDbType.VarChar).Value = request.tipo_usuario;
                comando.Parameters.Add("@contrasenia", MySqlDbType.VarChar).Value = request.contrasenia;


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


        [HttpPut]
        [Route("Actualizar_Correo")]
        public bool Actualizar_Correo([FromBody] Personas request)
        {

            bool Persona_actualizada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("UPDATE personas " +
                    "SET nombre=@Nombre, apellido_pat=@Apellido_pat,apellido_mat=@apellido_mat, telefono=@Telefono, fecha_nacimiento=@Fecha_nacimiento, codigo_acceso=@codigo_acceso " +
                    "WHERE correo=@correo", conexion);

            //    comando.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = request.id_persona;
                comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = request.nombre;
                comando.Parameters.Add("@apellido_pat", MySqlDbType.VarChar).Value = request.apellido_pat;
                comando.Parameters.Add("@apellido_mat", MySqlDbType.VarChar).Value = request.apellido_mat;
                comando.Parameters.Add("@telefono", MySqlDbType.VarChar).Value = request.telefono;
                comando.Parameters.Add("@fecha_nacimiento", MySqlDbType.Date).Value = request.fecha_nacimiento1;
                comando.Parameters.Add("@correo", MySqlDbType.VarChar).Value = request.correo;
                comando.Parameters.Add("@codigo_acceso", MySqlDbType.VarChar).Value = request.codigo_acceso;


                //    comando.Parameters.Add("@Tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;
                //   comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                //  comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_lote;
                //  comando.Parameters.Add("@intercomunicador", MySqlDbType.Int32).Value = request.intercomunicador;
                //  comando.Parameters.Add("@codigo_acceso", MySqlDbType.VarChar).Value = request.codigo_acceso;
                //   comando.Parameters.Add("@tipo_usuario", MySqlDbType.VarChar).Value = request.tipo_usuario;


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

        [HttpGet]
        [Route("Consultar_Persona")]
        //static public async Task<int>
        public List<Personas> Consultar_Persona(int id_administrador)
        {
            List<Personas> Persona = new List<Personas>();



            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas where id_administrador=@id_administrador", conexion);

                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = id_administrador;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        //  DateTime ffecha = reader.GetDateTime(5);



                        DateTime ffecha = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5);


                        Persona.Add(new Personas()
                        {
                            id_persona = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido_pat = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            apellido_mat = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            id_lote = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                            telefono = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            fecha_nacimiento = ffecha.ToString("yyyy-MM-dd"),
                            correo = reader.IsDBNull(10) ? "" : reader.GetString(10),
                            contrasenia = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            id_fraccionamiento = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            tipo_usuario = reader.GetString(13),
                            //    intercomunicador = reader.GetInt32(8),
                            //    codigo_acceso = reader.GetString(9),
                            //    correo = reader.GetString(10),
                            //    contrasenia = reader.GetString(11),

                            hikvision = reader.GetString(15)



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

                return Persona;
            }
        }

        [HttpGet]
        [Route("Consultar_PersonaIndividual")]

        public List<Personas> Consultar_PersonaIndividual(int id_persona)
        {
            List<Personas> Persona = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas where id_persona=@id_persona", conexion);
                //y id_fraccionamiento
                //  comando.Parameters.Add("@Nombre", MySqlDbType.Int32).Value = nombre;
                //  comando.Parameters.Add("@Apellido_pat", MySqlDbType.Int32).Value = apellido_pat;
                comando.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = id_persona;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime ffecha = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5);


                        Persona.Add(new Personas()
                        {
                            id_persona = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido_pat = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            apellido_mat = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            id_lote = reader.IsDBNull(7) ? 0: reader.GetInt32(7),
                            telefono = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            fecha_nacimiento = ffecha.ToString("yyyy-MM-dd"),
                            correo = reader.IsDBNull(10) ? "" : reader.GetString(10),
                            contrasenia = reader.GetString(11),
                            id_fraccionamiento = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            tipo_usuario = reader.GetString(13)
                            /*    intercomunicador = reader.GetInt32(8),
                                codigo_acceso = reader.GetString(9),
                                correo = reader.GetString(10),
                                contrasenia = reader.GetString(11)
                            */
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

                return Persona;
            }
        }


        [HttpPost]
        [Route("Agregar_Administrador")]


        public bool Agregar_Persona(string nombre, string correo, string contrasenia, string town)
        {
            bool usuario_agregado = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into personas (Correo, Nombre, Contrasenia, id_administrador, Tipo_usuario, codigo_acceso) VALUES (@Correo, @nombre, @Contrasenia, @id_administrador, @Tipo_usuario, @codigo_acceso)", conexion);

                //@id_fraccionamiento, @Nombre_deuda, @Descripción, @Monto, @Fecha_corte, @Periodicidad_dias

                comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = nombre;
                comando.Parameters.Add("@Correo", MySqlDbType.VarChar).Value = correo;
                comando.Parameters.Add("@Contrasenia", MySqlDbType.VarChar).Value = contrasenia;
                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = "0";
                comando.Parameters.Add("@Tipo_usuario", MySqlDbType.VarChar).Value = "administrador";
                comando.Parameters.Add("@codigo_acceso", MySqlDbType.VarChar).Value = town;





                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        usuario_agregado = true;


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
                return usuario_agregado;
            }
        }


        [HttpGet]
        [Route("Obtener_Correo_Persona")]
        public string Obtener_Correo_Persona(int id_persona)
        {
            string correo = "";
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT correo FROM personas WHERE id_persona=@id_persona", conexion);

                comando.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = id_persona;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        correo = reader.GetString(0);
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

        [HttpGet]
        [Route("Consultar_Personas_Fraccionamiento")]

        public List<Personas> PersonasFraccionamiento(int id_fraccionamiento)
        {

            List<Personas> Persona = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE id_fraccionamiento=@id_fraccionamiento", conexion);

             //   comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = id_administrador;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime ffecha = reader.GetDateTime(5);

                        Persona.Add(new Personas()
                        {
                            id_persona = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido_pat = reader.GetString(2),
                            apellido_mat = reader.GetString(3),
                            id_lote = reader.GetInt32(7),
                            telefono = reader.GetString(4),
                            fecha_nacimiento = ffecha.ToString("yyyy-MM-dd"),
                            correo = reader.GetString(10),
                            contrasenia = reader.GetString(11),
                            id_fraccionamiento = reader.GetInt32(6),
                            tipo_usuario = reader.GetString(13)
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

                return Persona;
            }

        }

        [HttpGet]
        [Route("Consultar_Personas_Por_Fraccionamiento")]

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
                        Lista_Personas.Add(new Personas() { id_persona = reader.GetInt32(0), nombre = reader.GetString(1), apellido_pat = reader.GetString(2), apellido_mat = reader.GetString(3), telefono = reader.GetString(4), fecha_nacimiento = reader.GetString(5), id_fraccionamiento = reader.GetInt32(6), id_lote = reader.GetInt32(7), intercomunicador = reader.GetInt32(8), codigo_acceso = reader.GetString(9), correo = reader.GetString(10), tipo_usuario = reader.GetString(13) });
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


        [HttpGet]
        [Route("Consultar_Ultima_Persona")]

        public List<Personas> Consultar_Ultima_Persona(int id_fraccionamiento)
        {
           List<Personas> Lista_Personas = new List<Personas>();
          //  int id_persona = 0;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas ORDER BY id_persona DESC LIMIT 1;", conexion);

                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = id_fraccionamiento;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_Personas.Add(new Personas()
                         {
                      //  return reader.GetInt32(0);
                     id_persona = reader.GetInt32(0),
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

                return Lista_Personas;
            }
        }



        [HttpGet]
        [Route("Consultar_Personas_Por_Lote")]

        public List<Personas> Consultar_Personas_Por_Lote(int id_persona)
        {
            List<Personas> Lista_Personas = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM personas WHERE id_persona=@id_persona", conexion);

                comando.Parameters.Add("@id_persona", MySqlDbType.Int32).Value = id_persona;



                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        Lista_Personas.Add(new Personas()
                        {
                            id_persona = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido_pat = reader.GetString(2),
                            apellido_mat = reader.GetString(3),
                            telefono = reader.GetString(4),
                            tipo_usuario = reader.GetString(13),
                            id_fraccionamiento = reader.GetInt32(6),
                            id_lote = reader.GetInt32(7),
                            correo = reader.GetString(10)
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

                return Lista_Personas;
            }
        }







        /*

        [HttpGet]
        [Route("Consultar")]

        public List<Personas> Consultar(int id_persona)
        {
            List<Personas> Persona = new List<Personas>();

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {

                MySqlCommand comando = new MySqlCommand("SELECT * FROM lotes where id_propietario =@id_administrador", conexion);
                //y id_fraccionamiento
                //  comando.Parameters.Add("@Nombre", MySqlDbType.Int32).Value = nombre;
                //  comando.Parameters.Add("@Apellido_pat", MySqlDbType.Int32).Value = apellido_pat;
                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = id_administrador;


                try
                {

                    conexion.Open();

                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime ffecha = reader.GetDateTime(5);

                        Persona.Add(new Personas()
                        {
                            id_persona = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            apellido_pat = reader.GetString(2),
                            apellido_mat = reader.GetString(3),
                            id_lote = reader.GetInt32(7),
                            telefono = reader.GetString(4),
                            fecha_nacimiento = ffecha.ToString("yyyy-MM-dd"),
                            correo = reader.GetString(10),
                            contrasenia = reader.GetString(11),
                            id_fraccionamiento = reader.GetInt32(6),
                            tipo_usuario = reader.GetString(13)
                            /*    intercomunicador = reader.GetInt32(8),
                                codigo_acceso = reader.GetString(9),
                                correo = reader.GetString(10),
                                contrasenia = reader.GetString(11)
                            */
        /*
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

                return Persona;
            }
        }

        */
    }

}


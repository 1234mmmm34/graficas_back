using API_Archivo.Clases;
using CardManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {


        [HttpPost]
        [Route("Agregar_Usuario")]

        public bool Agregar_Usuario([FromBody] Personas request)
        {
            bool Persona_agregada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into personas (nombre, apellido_pat, apellido_mat, telefono, fecha_nacimiento,  id_fraccionamiento, id_lote, correo, contrasenia, id_administrador, tipo_usuario, hikvision) VALUES ( @Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @id_fraccionamiento, @id_lote, @correo, @contrasenia, @id_administrador, @tipo_usuario, @hikvision)", conexion);

                //@Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @Tipo_usuario, @id_fraccionamiento, @id_lote, @Intercomunicador, @Codigo_acceso

                comando.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = request.nombre;
                comando.Parameters.Add("@Apellido_pat", MySqlDbType.VarChar).Value = request.apellido_pat;
                comando.Parameters.Add("@Apellido_mat", MySqlDbType.VarChar).Value = request.apellido_mat;
                comando.Parameters.Add("@Telefono", MySqlDbType.VarChar).Value = request.telefono;
                comando.Parameters.Add("@Fecha_nacimiento", MySqlDbType.DateTime).Value = request.fecha_nacimiento1;
                //    comando.Parameters.Add("@Tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_lote;
                comando.Parameters.Add("@correo", MySqlDbType.VarChar).Value = request.correo;
                comando.Parameters.Add("@contrasenia", MySqlDbType.VarChar).Value = request.contrasenia;
                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = request.id_administrador;
                comando.Parameters.Add("@tipo_usuario", MySqlDbType.VarChar).Value = request.tipo_usuario;
                comando.Parameters.Add("@hikvision", MySqlDbType.VarChar).Value = request.hikvision;


                int id_administrador = request.id_administrador.Value;

                DateTime now = DateTime.Now;
                DateTime Dateproximo_pago = DateTime.Now.AddDays(90);
                string fechaProximoPago = Dateproximo_pago.ToString("yyyy-MM-ddTHH:mm:ss");
                string fechaActual = now.ToString("yyyy-MM-ddTHH:mm:ss");



                try
                {
                    conexion.Open();
                    rowsaffected = comando.ExecuteNonQuery();

                    if (rowsaffected >= 1)
                    {
                        Persona_agregada = true;
                        AddDevice.Login("admin", "Repara123", "5551", "187.216.118.73");
                        // AddDevice.InsertUser(request.id_usuario.ToString());
                        string ultima = Consultar_Ultima_Persona(id_administrador).ToString();
                        AddDevice.InsertUser(ultima, request.nombre, fechaActual, fechaProximoPago);

                    }
                   // Consultar_Ultima_Persona(id_administrador).ToString()

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


        [HttpPut]
        [Route("Actualizar_Contrasenia")]
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

        [HttpGet]
        [Route("Consultar_Personas_Por_Fraccionamientos")]
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
                        Lista_Personas.Add(new Personas()
                        {
                            id_persona = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                            nombre = reader.IsDBNull(1) ? "" : reader.GetString(1),
                            apellido_pat = reader.IsDBNull(2) ? "" : reader.GetString(2),
                            apellido_mat = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            telefono = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            tipo_usuario = reader.IsDBNull(13) ? "" : reader.GetString(13),
                            id_fraccionamiento = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                            id_lote = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                            correo = reader.IsDBNull(10) ? "" : reader.GetString(10),
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
        [Route("Generar_Token")]
        public IActionResult GenerarToken()
        {
            var token = Guid.NewGuid().ToString(); // Generar un token aleatorio utilizando Guid
            return new ContentResult
            {
                Content = token,
                ContentType = "text/plain",
                StatusCode = 200 // Código de estado OK (200)
            };
        }

        [HttpPost]
        [Route("Generar_invitacion")]
        public bool Generar_invitacion(string token, string correo_invitado, int id_fraccionamiento, string nombre_fraccionamiento, string nombre_admin, string tipo_usuario)
        {
            bool invitacion_agregada = false;
            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into invitaciones (token, correo_invitado, id_fraccionamiento, nombre_fraccionamiento,nombre_administrador,tipo_usuario) VALUES (@token, @correo_invitado, @id_fraccionamiento, @nombre_fraccionamiento,@nombre_administrador,@tipo_usuario)", conexion);



                comando.Parameters.Add("@token", MySqlDbType.VarChar).Value = token;
                comando.Parameters.Add("@correo_invitado", MySqlDbType.VarChar).Value = correo_invitado;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.VarChar).Value = id_fraccionamiento;
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

        [HttpGet]
        [Route("Consultar_invitacion")]
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
                        Lista_invitacion.Add(new Invitaciones() { id_invitacion = reader.GetInt32(0), token = reader.GetString(1), correo_electronico = reader.GetString(2), id_fraccionamiento = reader.GetInt32(3), nombre_fraccionamiento = reader.GetString(5), nombre_admin = reader.GetString(6), tipo_usuario = reader.GetString(7) });
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

        [HttpGet]
        [Route("Consultar_Imagen")]
        public IActionResult Consultar_Imagen(int id_Persona)
        {
            Usuarios obj_usuario = new Usuarios();

            byte[] imagenBytes = obj_usuario.Consultar_Imagen(id_Persona);

            // Devolver los bytes como contenido binario
            return File(imagenBytes, "image/jpeg"); // Cambia el tipo de contenido según el formato de tu imagen
        }
        

        [HttpPost]
        [Route("Actualizar_Imagen")]

        public string Actualizar_Imagen(IFormFile file, int id_persona)
        {

            if (file.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    byte[] archivoEnBytes = memoryStream.ToArray(); // Convertir a byte[]

                    // Aquí puedes usar 'archivoEnBytes' como necesites
                    Usuarios obj_usuario = new Usuarios();
                    if (obj_usuario.Cargar_Imagen(archivoEnBytes, id_persona))
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
        [Route("Consultar_Ultima_Persona")]

        public string Consultar_Ultima_Persona(int id_fraccionamiento)
        {
            // List<Personas> Lista_Personas = new List<Personas>();
            //  int id_persona = 0;
            string id_persona="0";

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
                        //   Lista_Personas.Add(new Personas()
                        //   {
                        //  return reader.GetInt32(0);
                        id_persona = reader.GetInt32(0).ToString();
                      //  });
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

                return id_persona;
            }
        }

        


    }
}
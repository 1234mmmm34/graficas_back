/*
using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    public class Usuarios
    {
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
                    "SET imagen=@imagen " +
                    "WHERE id_persona=@id_persona";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@imagen", imagen_recibida);
                        command.Parameters.AddWithValue("@id_persona", id_persona);
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
                    string query = "SELECT imagen FROM personas WHERE id_persona = @id_persona";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_persona", id_Persona);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal("imagen")))
                                {
                                    long size = reader.GetBytes(reader.GetOrdinal("imagen"), 0, null, 0, 0);
                                    imagen = new byte[size];
                                    reader.GetBytes(reader.GetOrdinal("imagen"), 0, imagen, 0, (int)size);
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
*/




using API_Archivo.Clases;
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

        public bool Agregar_Usuario([FromBody] Usuarios request)
        {
            bool Persona_agregada = false;

            using (MySqlConnection conexion = new MySqlConnection(Global.cadena_conexion))
            {
                int rowsaffected = 0;
                MySqlCommand comando = new MySqlCommand("insert into personas (Nombre, Apellido_pat, Apellido_mat, Telefono, Fecha_nacimiento,  id_fraccionamiento, id_lote, Correo, Contrasenia, id_administrador, Tipo_usuario) VALUES ( @Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @id_fraccionamiento, @id_lote, @correo, @contrasenia, @id_administrador, @tipo_usuario)", conexion);

                //@Nombre, @Apellido_pat, @Apellido_mat, @Telefono, @Fecha_nacimiento, @Tipo_usuario, @id_fraccionamiento, @id_lote, @Intercomunicador, @Codigo_acceso

                comando.Parameters.Add("@Nombre", MySqlDbType.VarChar).Value = request.nombre;
                comando.Parameters.Add("@Apellido_pat", MySqlDbType.VarChar).Value = request.apellido_pat;
                comando.Parameters.Add("@Apellido_mat", MySqlDbType.VarChar).Value = request.apellido_mat;
                comando.Parameters.Add("@Telefono", MySqlDbType.VarChar).Value = request.telefono;
                comando.Parameters.Add("@Fecha_nacimiento", MySqlDbType.DateTime).Value = request.fecha_nacimiento;
                //    comando.Parameters.Add("@Tipo_usuario", MySqlDbType.VarChar).Value = tipo_usuario;
                comando.Parameters.Add("@id_fraccionamiento", MySqlDbType.Int32).Value = request.id_fraccionamiento;
                comando.Parameters.Add("@id_lote", MySqlDbType.Int32).Value = request.id_lote;
                comando.Parameters.Add("@correo", MySqlDbType.VarChar).Value = request.correo;
                comando.Parameters.Add("@contrasenia", MySqlDbType.VarChar).Value = request.contrasenia;
                comando.Parameters.Add("@id_administrador", MySqlDbType.Int32).Value = request.id_administrador;
                comando.Parameters.Add("@tipo_usuario", MySqlDbType.VarChar).Value = request.tipo_usuario;



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


        [HttpPatch]
        [Route("Actualizar_Contrasenia")]
        public string Actualizar_Contrasenia(string correo, string contrasenia)
        {
            Personas obj_persona = new Personas();

            if (obj_persona.Actualizar_Contrasenia(correo, contrasenia))
            {
                return "Contrasenia actualizada";
            }
            else
            {
                return "Error al actualizar contrasenia";
            }
        }

        [HttpGet]
        [Route("Consultar_Personas_Por_Fraccionamiento")]
        public List<Personas> Consultar_Personas_Por_Fraccionamiento(int id_fraccionamiento)
        {
            Personas obj_persona = new Personas();
            List<Personas> list_Personas = obj_persona.Consultar_Personas_Por_Fraccionamiento(id_fraccionamiento);
            return list_Personas;
        }

        [HttpGet]
        [Route("Consultar_ArrendatariosYPropietarios")]
        public List<Personas> Consultar_ArrendatariosYPropietarios(int id_fraccionamiento)
        {
            Personas obj_persona = new Personas();
            List<Personas> list_Personas = obj_persona.Consultar_Personas_Por_Fraccionamiento(id_fraccionamiento);
            return list_Personas;
        }

        [HttpGet]
        [Route("Consultar_Usuario")]
        public List<Personas> Consultar_Usuario(string nombre, string apellido_pat, string apellido_mat)
        {


            Personas obj_persona = new Personas();
            List<Personas> list_Persona = obj_persona.Consultar_Persona(nombre, apellido_pat, apellido_mat);

            return list_Persona;

        }

        [HttpGet]
        [Route("Consultar_Correo")]
        public ContentResult Consultar_Correo(string id_persona)
        {
            Personas obj_persona = new Personas();
            string correo = obj_persona.Obtener_Correo_Persona(Convert.ToInt32(id_persona));

            return new ContentResult
            {
                Content = correo,
                ContentType = "text/plain",
                StatusCode = 200 // Código de estado OK (200)
            };
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
        }//

        [HttpPost]
        [Route("Generar_Invitacion")]
        public string Generar_invitacion(string token, string correo_electronico, int id_fraccionamiento, int id_lote, string nombre_fraccionamiento, string nombre_admin, string tipo_usuario)
        {
            Invitaciones obj_invitacion = new Invitaciones();
            if (obj_invitacion.Generar_invitacion(token, correo_electronico, id_fraccionamiento, id_lote, nombre_fraccionamiento, nombre_admin, tipo_usuario))
            {
                return "Invitacion generada correctamente";
            }
            else
            {
                return "Error al generar la invitacion";
            }

        }//

        [HttpGet]
        [Route("Consultar_Invitacion")]
        public List<Invitaciones> Consultar_Invitacion(string token)
        {
            Invitaciones obj_invitacion = new Invitaciones();
            List<Invitaciones> lista_invitacion = obj_invitacion.Cosultar_invitacion(token);

            return lista_invitacion;
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






    }
}
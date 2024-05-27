


using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    public class Personas
    {

        public int? id_persona { get; set; }
        public string nombre { get; set; }
        public string? apellido_pat { get; set; }
        public string? apellido_mat { get; set; }
        public string? telefono { get; set; }

        public int? id_fraccionamiento { get; set; }
        public int? id_lote { get; set; }
        public int? intercomunicador { get; set; }

        public string? codigo_acceso { get; set; }

        public string? fecha_nacimiento { get; set; }

        public DateTime fecha_nacimiento1 { get; set; }

        public string? correo { get; set; }
        public string? contrasenia { get; set; }

        public string? tipo_usuario { get; set; }

        public int? id_administrador { get; set; }

        public string? hikvision { get; set; }


    }

}
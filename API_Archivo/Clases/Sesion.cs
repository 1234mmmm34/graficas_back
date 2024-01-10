/*
using CardManagement;
using MySql.Data.MySqlClient;
using System.Threading;

namespace API_Archivo.Clases
{
    public class Sesion
    {
        
        public string correo { get; set; }
        public int id_usuario { get; set; }
        public string tipo_usuario { get; set; }

        public int id_fraccionamiento { get; set; }


    }
}
*/


using CardManagement;
using MySql.Data.MySqlClient;
using System.Threading;

namespace API_Archivo.Clases
{
    public class Sesion
    {

        public string correo { get; set; }
        public int id_usuario { get; set; }
        public string tipo_usuario { get; set; }

        public int id_fraccionamiento { get; set; }
        public int id_lote { get; set; }


    }
}
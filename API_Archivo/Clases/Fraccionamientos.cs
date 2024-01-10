using MySql.Data.MySqlClient;

namespace API_Archivo.Clases
{
    public class Fraccionamientos
    {

        public int id_fraccionamiento { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string coordenadas { get; set; }
        public int id_administrador { get; set; }
        public int id_tesorero { get; set; }
        public int dia_pago { get; set; }


    }
}
using API_Archivo.Controllers;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Threading;

namespace API_Archivo.Clases
{
    public class Propiedades
    {

        public int id_lote { get; set; }
        public int id_fraccionamiento { get; set; }
        public int id_propietario { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }

        public int id_renta { get; set; }

        public int id_administrador { get; set; }

        public string nombre { get; set; }

        public string nombre_renta { get; set; }



    }
}

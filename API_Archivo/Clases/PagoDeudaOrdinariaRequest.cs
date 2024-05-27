namespace API_Archivo.Clases
{
    public class PagoDeudaOrdinariaRequest
    {
        public int id_deudor { get; set; }
        public int id_deuda { get; set; }
        public int id_fraccionamiento { get; set; }
        public string proximo_pago { get; set; }
        public IFormFile file { get; set; }
    }
}

namespace MvcFormRecognizer.Models
{
    public class FacturaReconocida
    {
        public string Proveedor { get; set; }
        public string Fecha { get; set; }
        public string NumeroFactura { get; set; }
        public decimal? Total { get; set; }

    }
}

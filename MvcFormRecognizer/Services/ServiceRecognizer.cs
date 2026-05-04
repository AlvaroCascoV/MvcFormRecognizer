using Azure.AI.FormRecognizer.DocumentAnalysis;
using MvcFormRecognizer.Models;

namespace MvcFormRecognizer.Services
{
    public class ServiceRecognizer
    {
        private DocumentAnalysisClient client;

        public ServiceRecognizer(DocumentAnalysisClient client)
        {
            this.client = client;
        }

        //TENDREMOS UN METODO PARA DEVOLVER LA FACTURA RECONOCIDA
        //NUESTRO MODEL
        public async Task<FacturaReconocida> AnalizarFacturaAsync(Stream streamPdf)
        {
            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(Azure.WaitUntil.Completed, "prebuilt-invoice", streamPdf);
            var result = operation.Value;
            var document = result.Documents.FirstOrDefault();
            if (document == null)
            {
                return null;
            }
            else
            {
                //VendorName, InvoiceDate, InvoiceId, InvoiceTotal
                FacturaReconocida factura = new FacturaReconocida();
                if (document.Fields.TryGetValue("VendorName", out var proveedor))
                {
                    factura.Proveedor = proveedor.Content;
                }
                if (document.Fields.TryGetValue("InvoiceDate", out var fecha))
                {
                    factura.Fecha = fecha.Content;
                }
                if (document.Fields.TryGetValue("InvoiceId", out var numero))
                {
                    factura.NumeroFactura = numero.Content;
                }
                if (document.Fields.TryGetValue("InvoiceTotal", out var total) & decimal.TryParse(total.Content, out var t))
                {
                    factura.Total = t;
                }
                return factura;
            }
        }
    }
}

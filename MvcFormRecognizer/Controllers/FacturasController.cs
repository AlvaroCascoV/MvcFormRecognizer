using Microsoft.AspNetCore.Mvc;
using MvcFormRecognizer.Models;
using MvcFormRecognizer.Services;

namespace MvcFormRecognizer.Controllers
{
    public class FacturasController : Controller
    {
        private ServiceRecognizer service;

        public FacturasController(ServiceRecognizer service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                FacturaReconocida factura = await service.AnalizarFacturaAsync(stream);
                return View(factura);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //para obtener la info de la cadena de conexión del appsettings
using Microsoft.Data.SqlClient;
using DesarrolloWebI_CL1_GarciaalvizuriDiazIsaac.Models;

namespace DesarrolloWebI_CL1_GarciaalvizuriDiazIsaac.Controllers
{
    public class FacturasController : Controller
    {
        private readonly IConfiguration _config;
        private string cadena;

        //para indicar que conexión a BD de las disponibles en el appsettings usaremos
        public FacturasController(IConfiguration _config)
        {
            this._config = _config;
            this.cadena = _config["ConnectionStrings:connection"];
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

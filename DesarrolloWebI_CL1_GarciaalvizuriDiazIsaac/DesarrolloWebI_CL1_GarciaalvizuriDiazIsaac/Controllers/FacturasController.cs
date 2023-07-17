using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; //para obtener la info de la cadena de conexión del appsettings
using Microsoft.Data.SqlClient;
using DesarrolloWebI_CL1_GarciaalvizuriDiazIsaac.Models;
using System.Data;

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


        //función para ejecutar el SP GetFacturasPorProducto
        IEnumerable<Factura> GetFacturasPorProducto(string nombre)
        {
            List<Factura> facturas = new List<Factura>();

            using (SqlConnection connection = new SqlConnection(this.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_GetFacturasPorProducto", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmstrNombreProducto", nombre);

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    facturas.Add(new Factura()
                    {
                        idFactura = dr.GetInt32(0),
                        fechaEmision = dr.GetDateTime(1),
                        nombreProducto = dr.GetString(2),
                        preciounitario = dr.GetDecimal(3),
                        cantidad = dr.GetDecimal(4),
                        monto = dr.GetDecimal(5)
                    });
                }
            }
            return facturas;
        }

        //Action result SP GetFacturasPorProducto
        public async Task<IActionResult> GetFacturasPorProducto(string nombre = "", int pagina = 0)
        {
            if (nombre == null)
                nombre = "";

            IEnumerable<Factura> facturas = GetFacturasPorProducto(nombre);
            int filasPagina = 5;
            int totalFilas = facturas.Count();
            int numeroPaginas = totalFilas % filasPagina == 0 ? totalFilas / filasPagina : (totalFilas / filasPagina) + 1;

            ViewBag.pagina = pagina;
            ViewBag.numeroPaginas = numeroPaginas;
            ViewBag.nombre = nombre;

            return View(await Task.Run(() => facturas.Skip(pagina * filasPagina).Take(filasPagina)));

        }


        //funcion para sp_GetFacturasPorAnioCliente
        IEnumerable<Factura> GetFacturasPorAnioCliente(int anio, string nombre)
        {
            List<Factura> facturas = new List<Factura>();

            using (SqlConnection connection = new SqlConnection(this.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_GetFacturasPorAnioCliente", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmintAnio", anio);
                cmd.Parameters.AddWithValue("@prmstrRazonSocial", nombre);

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    facturas.Add(new Factura()
                    {
                        idFactura = dr.GetInt32(0),
                        fechaEmision = dr.GetDateTime(1),
                        razonSocial = dr.GetString(2),
                        total = dr.GetDecimal(3)
                    });
                }
            }
            return facturas;

        }

        //ActionResult para GetFacturasPorAnioCliente
        public async Task<IActionResult> GetFacturasPorAnioCliente(int anio = 0, string nombre = "", int pagina = 0)
        {
            if (nombre == null)
                nombre = "";
            if (anio == 0)
                anio = 0;

            IEnumerable<Factura> facturas = GetFacturasPorAnioCliente(anio, nombre);
            int filasPagina = 5;
            int totalFilas = facturas.Count();
            int numeroPaginas = totalFilas % filasPagina == 0 ? totalFilas / filasPagina : (totalFilas / filasPagina) + 1;

            ViewBag.pagina = pagina;
            ViewBag.numeroPaginas = numeroPaginas;
            ViewBag.nombre = nombre;

            return View(await Task.Run(() => facturas.Skip(pagina * filasPagina).Take(filasPagina)));

        }


        public IActionResult Index()
        {
            return View();
        }
    }
}

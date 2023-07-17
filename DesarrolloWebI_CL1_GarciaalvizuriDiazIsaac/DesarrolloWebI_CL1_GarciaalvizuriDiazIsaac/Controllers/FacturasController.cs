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


        //función para ejecutar el SP GetFacturasPorProducto
        IEnumerable<Factura> GetFacturasPorProducto(string nombre)
        {
            List<Factura> facturas = new List<Factura>();

            using (SqlConnection connection = new SqlConnection(this.cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_GetFacturasPorProducto", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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





        public IActionResult Index()
        {
            return View();
        }
    }
}

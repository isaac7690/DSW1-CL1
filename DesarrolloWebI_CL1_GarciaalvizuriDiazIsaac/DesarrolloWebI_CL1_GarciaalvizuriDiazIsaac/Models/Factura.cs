using System.ComponentModel.DataAnnotations;

namespace DesarrolloWebI_CL1_GarciaalvizuriDiazIsaac.Models
{
    public class Factura
    {
        [Display(Name = "ID")]
        public int idFactura { get; set; }
        [Display(Name = "Fecha de Emisión")]
        public DateTime fechaEmision { get; set; }
        [Display(Name = "Producto")]
        public string? nombreProducto { get; set; }
        [Display(Name = "Precio Unitario")]
        public decimal preciounitario { get; set; }
        [Display(Name = "Cantidad")]
        public decimal cantidad { get; set; }
        [Display(Name = "Monto")]
        public decimal monto { get; set; }  
    }
}

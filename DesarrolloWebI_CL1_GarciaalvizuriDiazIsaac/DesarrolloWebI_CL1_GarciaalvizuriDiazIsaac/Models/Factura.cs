namespace DesarrolloWebI_CL1_GarciaalvizuriDiazIsaac.Models
{
    public class Factura
    {
        public int idFactura { get; set; }
        public DateTime fechaEmision { get; set; }
        public string? nombreProducto { get; set; }
        public decimal preciounitario { get; set; } 
        public decimal cantidad { get; set; }   
        public decimal monto { get; set; }  
    }
}

using System.ComponentModel.DataAnnotations;

namespace DesarrolloWebI_CL1_GarciaalvizuriDiazIsaac.Models
{
    public class Cliente
    {
        [Display(Name = "ID Cliente")]
        public int idCliente { get; set; }
        [Display(Name = "Cliente")]
        public string? razonSocial { get; set; }
        [Display(Name = "Dirección")]
        public string? direccion { get; set; }
        [Display(Name = "Teléfono")]
        public string? telefono { get; set; }
        [Display(Name = "ID Categoría")]
        public int idCategoriaCliente { get; set; }
        [Display(Name = "Categoría")]
        public string? nombreCategoria { get; set; }

    }
}

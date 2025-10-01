using System;
using System.ComponentModel.DataAnnotations;

namespace OrdersApi.Dtos
{
    public class OrderUpdateDto
    {
        [Required]
        [StringLength(200)]
        public string Cliente { get; set; } = string.Empty;


        public DateTime? Fecha { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor que 0.")]
        public decimal Total { get; set; }
    }
}
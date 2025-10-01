using System;
using System.ComponentModel.DataAnnotations;

namespace MiniOrdersApi.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Cliente { get; set; } = string.Empty;

        [Required]
        public DateTime? Fecha { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
        public decimal Total { get; set; }
    }
}
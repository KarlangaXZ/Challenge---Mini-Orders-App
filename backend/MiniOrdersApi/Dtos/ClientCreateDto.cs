using System.ComponentModel.DataAnnotations;

namespace MiniOrdersApi.Dtos
{
    public class ClientCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;
    }
}

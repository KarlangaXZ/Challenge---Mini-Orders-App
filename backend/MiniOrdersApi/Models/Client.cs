using System;
using System.ComponentModel.DataAnnotations;

namespace MiniOrdersApi.Models
{
    public class Client
    {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = default!;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    }
}

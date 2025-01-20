using System.ComponentModel.DataAnnotations;

namespace WebshopApi.DTOs;
public class CartItemDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }

    [Required]
    public required string SessionId { get; set; }
}
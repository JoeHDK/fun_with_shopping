using WebshopApi.DTOs;
using WebshopApi.Models;

namespace WebshopApi.Mappers;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price
        };
    }

    public static IEnumerable<ProductDto> ToDtoList(IEnumerable<Product> products)
    {
        return products.Select(ToDto);
    }
}
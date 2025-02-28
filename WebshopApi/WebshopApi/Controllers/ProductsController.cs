﻿using Microsoft.AspNetCore.Mvc;
using WebshopApi.Mappers;
using WebshopApi.Models;
using WebshopApi.Services;

namespace WebshopApi.Controllers;
[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "Products")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = productService.GetAllProducts();
        var productDtos = ProductMapper.ToDtoList(products);
        return Ok(productDtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetProduct(int id)
    {
        var product = productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        var productDto = ProductMapper.ToDto(product);
        return Ok(productDto);
    }
    
    [HttpGet("search")]
    public IActionResult SearchProducts(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var results = productService.SearchProducts(query);
        return Ok(results);
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        productService.AddProduct(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id, Product product)
    {
        productService.UpdateProduct(id, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        productService.DeleteProduct(id);
        return NoContent();
    }
}
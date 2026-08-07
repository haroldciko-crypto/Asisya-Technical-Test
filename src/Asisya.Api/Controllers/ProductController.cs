using Asisya.Application.DTOs.Product;
using Asisya.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Asisya.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductGeneratorService _productGeneratorService;

    public ProductController(IProductService productService, IProductGeneratorService productGeneratorService)
    {
        _productService = productService;
        _productGeneratorService = productGeneratorService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var product = await _productService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.ProductID },
            product);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] ProductFilterRequest filter)
    {
        var products = await _productService.GetPagedAsync(filter);

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductRequest request)
    {
        await _productService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateProducts([FromQuery] int quantity = 100000)
    {
        if (quantity <= 0)
        {
            return BadRequest("La cantidad debe ser mayor que cero.");
        }

        var stopwatch = Stopwatch.StartNew();

        var inserted = await _productGeneratorService.GenerateAsync(quantity);

        stopwatch.Stop();

        return Ok(new
        {
            Message = "Productos generados correctamente.",
            Inserted = inserted,
            ElapsedSeconds = Math.Round(stopwatch.Elapsed.TotalSeconds, 2)
        });
    }
}
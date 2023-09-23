using Microsoft.AspNetCore.Mvc;
using MongoExample.API.Models;
using MongoExample.API.Models.Product;
using MongoExample.API.Services.Interfaces;

namespace MongoExample.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseModel<ProductResponseModel>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> AddAsync([FromBody] ProductRequestModel requestModel)
    {
        var result = await _productService.AddProductAsync(requestModel);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<ProductListResponseModel>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _productService.GetAllAsync();

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ProductListResponseItem>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> GetAllAsync([FromRoute] string id)
    {
        var result = await _productService.GetByIdAsync(id);

        return StatusCode(result.StatusCode, result);
    }
}
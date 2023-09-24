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
    [ProducesResponseType(typeof(ResponseModel<>), 400)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> AddAsync([FromBody] ProductRequestModel requestModel)
    {
        var result = await _productService.AddProductAsync(requestModel);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<ProductListResponseModel>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 400)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _productService.GetAllAsync();

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ProductListResponseItem>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 400)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var result = await _productService.GetByIdAsync(id);

        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ProductDeleteResponseModel>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 400)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] string id)
    {
        var result = await _productService.DeleteAsync(id);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseModel<ProductUpdateResponseModel>), 200)]
    [ProducesResponseType(typeof(ResponseModel<>), 400)]
    [ProducesResponseType(typeof(ResponseModel<>), 500)]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] ProductRequestModel requestModel)
    {
        var result = await _productService.UpdateAsync(id, requestModel);

        return StatusCode(result.StatusCode, result);
    }
}
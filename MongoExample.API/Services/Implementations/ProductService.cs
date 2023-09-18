using MongoExample.API.Models;
using MongoExample.API.Models.Product;
using MongoExample.API.Services.Interfaces;
using MongoExample.Core.Entities;
using MongoExample.Data.Context;

namespace MongoExample.API.Services.Implementations;

public class ProductService : IProductService
{
    private readonly MongoDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(MongoDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ResponseModel<ProductResponseModel>> AddProductAsync(ProductRequestModel model)
    {
        try
        {
            var response = new ProductResponseModel();
            
            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
            };

           await _context.Products.InsertOneAsync(product);

           response.Message = "Product added successfully";
           return ResponseModel<ProductResponseModel>.SendSuccess(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product");
            return ResponseModel<ProductResponseModel>.SendException(ex);
        }
    }
}
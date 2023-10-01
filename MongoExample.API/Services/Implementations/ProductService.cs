using MongoDB.Driver;
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
    private readonly IConfiguration _configuration;

    public ProductService(MongoDbContext context, ILogger<ProductService> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<ResponseModel<ProductResponseModel>> AddProductManuelAsync(ProductRequestModel model)
    {
        try
        {
            var response = new ProductResponseModel();
            var mongoClient = new MongoClient(_configuration.GetConnectionString("MongoDb"));

            var database = mongoClient.GetDatabase(_configuration["MongoDbSettings:DatabaseName"]);

            var collection = database.GetCollection<Product>(nameof(Product));

            await collection.InsertOneAsync(new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
            });

            response.Message = "Product added successfully";
            return ResponseModel<ProductResponseModel>.SendSuccess(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product");
            return ResponseModel<ProductResponseModel>.SendException(ex);
        }
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

    public async Task<ResponseModel<ProductListResponseModel>> GetAllAsync()
    {
        try
        {
            var response = new ProductListResponseModel();

            var data = await _context.Products.FindAsync(FilterDefinition<Product>.Empty);

            var list = await data.ToListAsync();

            response.Products = list.Select(x => new ProductListResponseItem()
            {
                Id = x.Id,
                Name = x.Name,
                Quantity = x.Quantity,
                Price = x.Price,
            }).ToList();
            response.TotalCount = list.Count;

            return ResponseModel<ProductListResponseModel>.SendSuccess(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get all product");
            return ResponseModel<ProductListResponseModel>.SendException(ex);
        }
    }

    public async Task<ResponseModel<ProductListResponseItem>> GetByIdAsync(string id)
    {
        try
        {
            var data = await _context.Products.FindAsync(x => x.Id == id);

            var product = await data.FirstOrDefaultAsync();
            var response = new ProductListResponseItem
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
            };

            return ResponseModel<ProductListResponseItem>.SendSuccess(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error get product {id}", id);
            return ResponseModel<ProductListResponseItem>.SendException(ex);
        }
    }

    public async Task<ResponseModel<ProductDeleteResponseModel>> DeleteAsync(string id)
    {
        try
        {
            var response = new ProductDeleteResponseModel();

            await _context.Products.DeleteOneAsync(x => x.Id == id);

            response.Message = "Product deleted successfully";
            return ResponseModel<ProductDeleteResponseModel>.SendSuccess(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error delete product {id}", id);
            return ResponseModel<ProductDeleteResponseModel>.SendException(ex);
        }
    }

    public async Task<ResponseModel<ProductUpdateResponseModel>> UpdateAsync(string id, ProductRequestModel model)
    {
        try
        {
            var response = new ProductUpdateResponseModel();

            var update = Builders<Product>.Update
                .Set(x => x.Name, model.Name)
                .Set(x => x.Quantity, model.Quantity)
                .Set(x => x.Price, model.Price);

            await _context.Products.UpdateOneAsync(x => x.Id == id, update);

            response.Message = "Product updated successfully";
            return ResponseModel<ProductUpdateResponseModel>.SendSuccess(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error update product {id}", id);
            return ResponseModel<ProductUpdateResponseModel>.SendException(ex);
        }
    }
}
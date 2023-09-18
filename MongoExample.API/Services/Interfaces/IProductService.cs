using MongoExample.API.Models;
using MongoExample.API.Models.Product;

namespace MongoExample.API.Services.Interfaces;

public interface IProductService
{
    Task<ResponseModel<ProductResponseModel>> AddProductAsync(ProductRequestModel model);
}
namespace MongoExample.API.Models.Product;

public class ProductListResponseModel
{
    public int TotalCount { get; set; }
    public List<ProductListResponseItem> Products { get; set; }

    public ProductListResponseModel()
    {
        Products = Enumerable.Empty<ProductListResponseItem>().ToList();
    }
}

public class ProductListResponseItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
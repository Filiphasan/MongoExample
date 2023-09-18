namespace MongoExample.API.Models.Product;

public class ProductRequestModel
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
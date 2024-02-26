namespace Product.API.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> CreateProduct(ProductEntity product);
        Task<ProductEntity> GetProduct(string productId);
    }
}


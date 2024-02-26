namespace Product.API.Implementations.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }

        public async Task<ProductEntity> CreateProduct(ProductEntity product)
        {
            product.ProductId = UtilityHelper.GenerateUniqueID();

            EntityEntry<ProductEntity> entry = productDbContext.Products.Add(product);
            await productDbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<ProductEntity> GetProduct(string productId)
        {
            ProductEntity product = await productDbContext.Products.AsNoTracking()
                .Where(p => p.ProductId.Equals(productId))
                .FirstOrDefaultAsync() ?? throw new NotFoundException($"Product with id: {productId} was not found");

            return product;
        }
    }
}

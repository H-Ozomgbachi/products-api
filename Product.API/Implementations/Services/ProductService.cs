namespace Product.API.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<Result<ProductModel>> CreateProduct(CreateProductPayload createProduct)
        {
            ProductEntity product = mapper.Map<ProductEntity>(createProduct);

            ProductEntity createdProduct = await productRepository.CreateProduct(product);

            return new Result<ProductModel>()
            {
                ResponseDetails = mapper.Map<ProductModel>(createdProduct)
            };
        }

        public async Task<Result<ProductModel>> GetProduct(string productId)
        {
            ProductEntity product = await productRepository.GetProduct(productId);

            return new Result<ProductModel>()
            {
                ResponseDetails = mapper.Map<ProductModel>(product)
            };
        }
    }
}

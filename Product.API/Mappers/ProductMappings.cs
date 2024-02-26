namespace Product.API.Mappers
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<ProductEntity, ProductModel>().ReverseMap();
            CreateMap<ProductEntity, CreateProductPayload>().ReverseMap();
        }
    }
}


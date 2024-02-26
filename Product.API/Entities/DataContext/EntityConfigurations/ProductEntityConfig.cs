namespace Product.API.Entities.DataContext.EntityConfigurations
{
    public class ProductEntityConfig : BaseEntityConfig<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("PRODUCTS");
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId).HasColumnName("PRODUCT_ID").HasMaxLength(50);
            builder.Property(p => p.Name).HasColumnName("PRODUCT_NAME").HasMaxLength(100);
            builder.Property(b => b.Price).HasColumnName("PRODUCT_PRICE").HasPrecision(14,2);
        }
    }
}


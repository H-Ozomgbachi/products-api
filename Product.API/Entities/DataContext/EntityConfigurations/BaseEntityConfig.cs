namespace Product.API.Entities.DataContext.EntityConfigurations
{
    public abstract class BaseEntityConfig<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.Property(b => b.TimeCreated).HasColumnName("TIME_CREATED");
            builder.Property(b => b.CreatedBy).HasColumnName("CREATED_BY").HasMaxLength(100);
            builder.Property(b => b.TimeModified).HasColumnName("TIME_MODIFIED");
            builder.Property(b => b.ModifiedBy).HasColumnName("MODIFIED_BY").HasMaxLength(100);
        }
    }
}

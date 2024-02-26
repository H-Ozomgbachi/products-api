namespace Product.API.Entities
{
    public class BaseEntity
    {
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "Sys";
        public DateTime TimeModified { get; set; } = DateTime.MinValue;
        public string ModifiedBy { get; set; }
    }
}

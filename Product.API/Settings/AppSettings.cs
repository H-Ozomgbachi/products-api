namespace Product.API.Settings
{
    public record AppSettings
    {
        public string EKey { get; set; }
        public string EIv { get; set; }
    }
}

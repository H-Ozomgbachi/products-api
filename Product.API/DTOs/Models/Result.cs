namespace Product.API.DTOs.Models
{
    public record Result<T>
    {
        public string ResponseCode { get; set; } = CustomResponseCode.Ok;
        public string ResponseMsg { get; set; } = CustomResponseMsg.Ok;
        public T ResponseDetails { get; set; }
    }
}


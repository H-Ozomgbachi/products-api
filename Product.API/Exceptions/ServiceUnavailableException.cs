namespace Product.API.Exceptions
{
    public class ServiceUnavailableException : BaseException
    {
        public ServiceUnavailableException() : base(HttpStatusCode.ServiceUnavailable)
        {
        }

        public ServiceUnavailableException(string message) : base(HttpStatusCode.ServiceUnavailable, message)
        {
        }
    }
}
namespace Product.API.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base(HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message, bool isValidationProblems = false) : base(HttpStatusCode.BadRequest, message, isValidationProblems)
        {
        }
    }
}


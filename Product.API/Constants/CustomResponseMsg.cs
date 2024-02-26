namespace Product.API.Constants
{
    public struct CustomResponseMsg
    {
        public const string Ok = "Success";
        public const string InternalServer = "Something went wrong";
        public const string Conflict = "Duplicate operation";
        public const string Unauthorized = "Access denied";
        public const string ServiceUnavailable = "An external service was not available";
        public const string ValidationError = "One or more validation error occurred";
    }
}
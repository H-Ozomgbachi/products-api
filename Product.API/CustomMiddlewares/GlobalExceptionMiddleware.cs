namespace Product.API.CustomMiddlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly CryptographyHelper cryptography;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, CryptographyHelper cryptography)
        {
            _next = next;
            _logger = logger;
            this.cryptography = cryptography;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            _logger.LogError($"Error Processing Request\nMessage: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)GetStatusCode(ex);

            string errorMessage = ex.InnerException?.Message ?? ex.Message;

            var res = new Result<object>();

            if (ex is BaseException)
            {
                BaseException baseException = ex as BaseException;

                res.ResponseCode = GetResponseCode(baseException?.StatusCode ?? HttpStatusCode.InternalServerError);
                res.ResponseMsg = baseException?.Message ?? CustomResponseMsg.InternalServer;
                res.ResponseDetails = null;

                if (baseException?.IsValidationProblems ?? false)
                {
                    string[] validationFailures = errorMessage.Split('|');
                    res.ResponseMsg = CustomResponseMsg.ValidationError;
                    res.ResponseDetails = validationFailures;
                }
            }
            else
            {
                res.ResponseCode = GetResponseCode(HttpStatusCode.InternalServerError);
                res.ResponseMsg = CustomResponseMsg.InternalServer;
                res.ResponseDetails = errorMessage;
            }

            var options = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(res, options));
        }

        private static HttpStatusCode GetStatusCode(Exception ex)
        {
            if (ex is not BaseException internalException)
            {
                return HttpStatusCode.InternalServerError;
            }
            return internalException.StatusCode;
        }
        private static string GetResponseCode(HttpStatusCode statusCode)
        {
            string value = (int)statusCode switch
            {
                400 => CustomResponseCode.BadRequest,
                401 => CustomResponseCode.Unauthorized,
                404 => CustomResponseCode.NotFound,
                409 => CustomResponseCode.Conflict,
                503 => CustomResponseCode.ServiceUnavailable,
                _ => CustomResponseCode.InternalServer,
            };
            return value;
        }
    }
}


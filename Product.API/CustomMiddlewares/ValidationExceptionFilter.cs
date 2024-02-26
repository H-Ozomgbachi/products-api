namespace Product.API.CustomMiddlewares
{
    public class ValidationExceptionFilter : ActionFilterAttribute
    {
        private readonly ILogger<ValidationExceptionFilter> _logger;

        public ValidationExceptionFilter(ILogger<ValidationExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                _logger.LogError($"Validation error occurred: {UtilityHelper.Serializer(errors)}");

                var responseObj = new Result<object>
                {
                    ResponseCode = CustomResponseCode.BadRequest,
                    ResponseMsg = CustomResponseMsg.ValidationError,
                    ResponseDetails = errors
                };

                context.Result = new JsonResult(responseObj);
            }
        }
    }
}


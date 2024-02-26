namespace Product.API.CustomMiddlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var requestBodyStream = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            string requestBodyText = await new StreamReader(requestBodyStream).ReadToEndAsync();

            _logger.LogInformation($"\nIncoming Request -\n \tMethod: {context.Request.Method}\n \tPath: {context.Request.Path}\n \tBody: {requestBodyText}\n \tRequest Time: {DateTime.UtcNow}\n \tHeaders: {UtilityHelper.Serializer(context.Request.Headers)}\n");

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            // Capture the response body
            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(responseBodyStream).ReadToEndAsync();

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalBodyStream);

            _logger.LogInformation($"\nOutgoing Response - \n \tStatus Code: {context.Response.StatusCode}\n \tBody: {responseBodyText}\n \tResponse Time: {DateTime.UtcNow}\n \tHeaders: {UtilityHelper.Serializer(context.Request.Headers)}\n");
        }
    }
}


using System;

namespace Product.API.CustomMiddlewares
{
    public class EncryptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly CryptographyHelper cryptography;
        private readonly AppSettings appSettings;

        public EncryptionMiddleware(RequestDelegate next, IOptions<AppSettings> options, CryptographyHelper cryptography)
        {
            this.next = next;
            this.cryptography = cryptography;
            this.appSettings = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            // Decrypt incoming request
            using MemoryStream ms = new();
            await context.Request.Body.CopyToAsync(ms);

            string x = Encoding.ASCII.GetString(ms.ToArray());
            string decryptedRequestString = cryptography.DecryptString(x);
            context.Request.Body = new MemoryStream(Encoding.ASCII.GetBytes(decryptedRequestString));

            // Capture the response stream
            Stream originalBody = context.Response.Body;
            using MemoryStream responseBody = new();
            context.Response.Body = responseBody;

            await next(context);

            // Encrypt outgoing response
            responseBody.Seek(0, SeekOrigin.Begin);
            string responseContent = new StreamReader(responseBody).ReadToEnd();
            string encryptedResponseString = cryptography.EncryptString(responseContent);

            // Write the encrypted response back to the original response stream
            context.Response.Body = originalBody;
            byte[] encryptedResponseBytes = Encoding.ASCII.GetBytes(encryptedResponseString);
            await context.Response.Body.WriteAsync(encryptedResponseBytes);

        }
    }
}

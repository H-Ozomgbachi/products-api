namespace Product.API.Controllers.v1
{
    public class ProductsController : BaseControllerV1
    {
        private readonly IProductService productService;
        private readonly CryptographyHelper cryptography;

        public ProductsController(IProductService productService, CryptographyHelper cryptography)
        {
            this.productService = productService;
            this.cryptography = cryptography;
        }

        [HttpPost("products/create-product")]
        public async Task<ActionResult<Result<ProductModel>>> CreateProduct([FromBody]CreateProductPayload createProduct)
        {
            var response = await productService.CreateProduct(createProduct);

            return Ok(response);
        }

        [HttpGet("products/{productId}")]
        public async Task<ActionResult<Result<ProductModel>>> GetProduct(string productId)
        {
            var response = await productService.GetProduct(productId);

            return Ok(response);
        }

        [HttpPost("products/encrypt")]
        public IActionResult Encrypt([FromBody]string payload)
        {
            string response = cryptography.EncryptString(payload);
            return Ok(response);
        }

        [HttpPost("products/decrypt")]
        public IActionResult Decrypt([FromBody] string payload)
        {
            string response = cryptography.DecryptString(payload);
            return Ok(response);
        }
    }
}


using Microsoft.OpenApi.Models;

namespace Product.API
{
    public static class ServicesExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Products API", Version = "v1"});
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(v =>
            {
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.ReportApiVersions = true;
                v.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("api-version"));
            });
        }

        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }

        public static void ConfigureOtherServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<AppSettings>().Bind(config.GetSection("AppSettings"));

            services.AddSingleton<CryptographyHelper>();

            string connectionString = config["AppSettings:DbConnection"];

            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(connectionString));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateProductPayloadValidator>();

            services.AddAutoMapper(typeof(ProductMappings).Assembly);
        }
    }
}


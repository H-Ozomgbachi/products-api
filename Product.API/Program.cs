var builder = WebApplication.CreateBuilder(args);

// Logger Setup
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
})
    .AddNewtonsoftJson(
        options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    );

builder.Services.AddEndpointsApiExplorer();

string[] origins = builder.Configuration["AppSettings:Origins"].Split(",");
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .WithOrigins(origins)
    .AllowAnyMethod().AllowAnyHeader());
});

builder.Services.ConfigureAppServices();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureOtherServices(builder.Configuration);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint(builder.Configuration["AppSettings:SwaggerEndpoint"], "Products API");
});

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
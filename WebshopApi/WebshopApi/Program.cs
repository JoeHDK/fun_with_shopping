using Microsoft.EntityFrameworkCore;
using WebshopApi.Data;
using WebshopApi.Repositories;
using WebshopApi.Services;
using WebshopApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderLineService, OrderLineService>();

// Add Repositories.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();

// Add DbContext
builder.Services.AddDbContext<WebshopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen(c =>
{
    c.TagActionsBy(api => [api.GroupName]);
    c.DocInclusionPredicate((name, api) => true);
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Webshop API",
        Version = "v1",
        Description = "API for managing the Webshop"
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
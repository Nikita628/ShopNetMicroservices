using Basket.Application.Models;
using Basket.Application.Repositories;
using Basket.Application.Services;
using Discount.Grpc.Protos;
using static Discount.Grpc.Protos.DiscountProtoService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache((opt) =>
{
    var settings = builder.Configuration.GetRequiredSection("CacheSettings").Get<CacheSettings>();
    opt.Configuration = settings.ConnectionString;
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => {
    var settings = builder.Configuration.GetRequiredSection("GrpcSettings").Get<GrpcSettings>();
    o.Address = new Uri(settings.DiscountUri);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

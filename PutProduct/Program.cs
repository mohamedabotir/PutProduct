using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PutProduct.abstracts.Repository;
using PutProduct.Cores.Repository;
using PutProduct.Infrastructure.Extensions;
using PutProduct.Data;
using PutProduct.Services.jwt;
using Newtonsoft.Json;
using PutProduct.abstracts.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
    .AddIdentity()
    .JwtAuthentication(builder.Configuration)
    .AddSwagger();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IProductRepository,ProductRepository>();
builder.Services.AddTransient<IIdentityService, IdentityExtensions>();
builder.Services.AddCors();






builder.Services.AddControllers().AddNewtonsoftJson(c=>c.SerializerSettings.ReferenceLoopHandling=ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Error");

    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        o.RoutePrefix = string.Empty;

    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
 

app.UseCors(
    x => { x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

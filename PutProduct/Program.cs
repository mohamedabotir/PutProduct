using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PutProduct;
using PutProduct.Data;
using PutProduct.Services.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSettings"));

builder.Services.Configure<IdentityOptions>(o=>{
    o.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
});
builder.Services.AddIdentity<User, IdentityRole>(options =>
 options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddCors(); 
builder.Services.AddTransient<ApplicationDbContext>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(z => { 
    z.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}    
    );


// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.Run();

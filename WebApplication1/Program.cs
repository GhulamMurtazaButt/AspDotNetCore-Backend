using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Services.AuthService;
using WebApplication1.Services.AuthService.NewFolder;
using WebApplication1.Services.Email;
using WebApplication1.Services.ProductService;
using WebApplication1.Services.TokenService;
using WebApplication1.Services.UserService;
using DataLibrary;
using WebApplication1.Services.Email.Impl;
using WebApplication1.Services.ProductService.Impl;
using WebApplication1.Services.TokenService.Impl;
using WebApplication1.Services.UserService.Impl;
using WebApplication1.Utilities.Base64;
using WebApplication1.Utilities.Base64.Impl;
using DataLibrary.Repositry.Impl;
using WebApplication1.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
   x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped(typeof(DataLibrary.Repositry.IGenericRepositry<>), typeof(GenericRepositry<>));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBase64, Base64>();
builder.Services.AddIdentity<DataLibrary.Models.Users, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection(Constants.TokenSection).Value)),
            ValidateIssuer = false,
            ValidateAudience = false,

        };
});
var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

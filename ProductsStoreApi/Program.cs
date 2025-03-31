using Microsoft.EntityFrameworkCore;
using ProductsStore.Infraestructure.Persistence;
using MediatR;
using ProductsStore.Application.Features.Auth.Commands;
using ProductsStore.Infraestructure.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProductsStore.Application.Contracts;
using ProductsStore.Infraestructure.Services;
using ProductsStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using ProductsStore.Application.ConfigurationTemplates;
using ProductsStore.Infraestructure.Persistence.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using ProductsStoreApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtProvider, JwtBearerService>();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("AuthenticationSettings"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddDbContext<ProductsStoreDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly)); 

builder.Services.AddAutoMapper(typeof(AuthProfiles).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthenticationSettings:Key"]))
        };
    });

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;

}).AddDefaultTokenProviders()
.AddEntityFrameworkStores<ProductsStoreDbContext>();

var developmentPolicy = new CorsPolicyBuilder()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .Build();
builder.Services.AddCors(options =>

{
    options.AddPolicy("develop", developmentPolicy);
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("develop");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

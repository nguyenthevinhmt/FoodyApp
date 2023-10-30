using Foody.Application;
using Foody.Application.Services.AuthServices.Implements;
using Foody.Application.Services.AuthServices.Interfaces;
using Foody.Application.Services.CartServices.Implements;
using Foody.Application.Services.CartServices.Interfaces;
using Foody.Application.Services.CategoryServices.Implements;
using Foody.Application.Services.CategoryServices.Interfaces;
using Foody.Application.Services.DashboardServices.Implements;
using Foody.Application.Services.DashboardServices.Interfaces;
using Foody.Application.Services.EmailServices;
using Foody.Application.Services.EmailServices.Dtos;
using Foody.Application.Services.FileStoreService.Implements;
using Foody.Application.Services.FileStoreService.Interfaces;
using Foody.Application.Services.OrderServices.Implements;
using Foody.Application.Services.OrderServices.Interfaces;
using Foody.Application.Services.ProductServices.Implements;
using Foody.Application.Services.ProductServices.Interfaces;
using Foody.Application.Services.PromotionServices.Implements;
using Foody.Application.Services.PromotionServices.Interfaces;
using Foody.Application.Services.UserServices.Implements;
using Foody.Application.Services.UserServices.Interfaces;
using Foody.Application.Services.VnpayService.Implements;
using Foody.Application.Services.VnpayService.Interfaces;
using Foody.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Foody.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddDbContext<FoodyAppContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("FoodyAppConnectionString"));
            });
            //Config JWT setting
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT")["Key"])),
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = builder.Configuration.GetSection("JWT")["ValidAudience"],
                    ValidIssuer = builder.Configuration.GetSection("JWT")["ValidIssuer"],
                    ClockSkew = TimeSpan.Zero
                };

            });

            builder.Services.AddOptions();
            builder.Services.Configure<AppSettings>(
                builder.Configuration.GetSection("Vnpay"));
            builder.Services.AddOptions();
            builder.Services.Configure<MailSettings>(
                builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                //Setting Xml for writing description API
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                //Config swagger for using Bearer Token
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Foody Web API"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            //Setting Cors
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IStorageService, FileStorageService>();
            builder.Services.AddScoped<IPromotionService, PromotionService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IVnpayService, VnpayService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ImageStorage", "images")),
                RequestPath = "/ImageStorage/images"
            });

            app.MapControllers();

            app.Run();
        }
    }
}
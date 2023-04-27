using _14_4_CodeFirst_WebApi_LibraryDb.Entities;
using _14_4_CodeFirst_WebApi_LibraryDb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace _14_4_CodeFirst_WebApi_LibraryDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,//uygulamada izin verilen sitenin denetlenip denetlenmeyeceðini söyler
                        ValidateIssuer = true,//hangi sitenin denetleyip denetlemeyeceðine karar verir
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"])),
                        ClockSkew = TimeSpan.FromMinutes(30)
                    };
                }
                );

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService,UserService>();
            
            builder.Services.AddControllers().AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                opt =>
                {
                    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Insert JWT Token",
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    opt.OperationFilter<SecurityRequirementsOperationFilter>();

                }


                );//Swaggerda Api Kontrolü Ýçin Yapýldý

            builder.Services.AddDbContext<LibraryDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("LibraryCon")));

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
        }
    }
}
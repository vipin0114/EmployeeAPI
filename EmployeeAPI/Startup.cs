using EmployeeAPI.Context;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProductApi;
using System;
using System.Collections.Generic;

namespace EmployeeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {

                options.AddPolicy("AnotherPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:44305")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddControllers();
            //services.AddMassTransit(cfg =>
            //{
            //    cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider));
            //});

            //services.AddMassTransitHostedService();

            services.AddDbContext<EmployeeDBContext>
         (o => o.UseSqlServer(Configuration.
          GetConnectionString("DefaultConnections")));

            //services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer("DefaultConnection"));

            TokenValidationConfig(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
          
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

                     

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void TokenValidationConfig(IServiceCollection services)
        {
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "http://localhost:50191",
                    ValidateAudience = true,
                    ValidAudience = "http://localhost:50191",
                    LifetimeValidator = LifetimeValidator


                };

            });
        }
        

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Order.Api.Extensions;
using Order.Application.Mapper;
using Order.Application.Models;
using Order.Domain.Interfaces.Repositories.DataConnector;
using Order.Infra.DataConnector;
using System.Linq;
using System.Text;

namespace Order
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
            services.AddAutoMapper(typeof(Core));
            services.AddControllers();

            var authSettingsSection = Configuration.GetSection("AuthSettings");
            services.Configure<AuthSettings>(authSettingsSection);

            var authSettings = authSettingsSection.Get<AuthSettings>();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Secret));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddPolicyScheme("programevc", "Authorization Bearer or AccessToken", options =>
                    {
                        options.ForwardDefaultSelector = context =>
                        {
                            if (context.Request.Headers["Access-Token"].Any())
                            {
                                return "Access-Token";
                            }

                            return JwtBearerDefaults.AuthenticationScheme;
                        };
                    }).AddJwtBearer(x =>
                    {
                        x.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = "programevc",

                            ValidateAudience = false,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,

                            // Verify if token is valid
                            ValidateLifetime = true,
                            RequireExpirationTime = true,

                        };

                    });


            string connectionString = Configuration.GetConnectionString("default");
            services.AddScoped<IDbConnector>(db => new SqlConnector(connectionString));

            services.RegisterIoC();

            services.SwaggerConfiguration();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.RoutePrefix = "swagger";
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Documentation");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

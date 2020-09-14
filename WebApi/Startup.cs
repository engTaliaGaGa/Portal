using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Text;
using WebApi.Singleton;
using WebApi.Singleton.Interface;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Settings settings = Configuration.GetSection("settings").Get<Settings>();
            //settings.Configure();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(
                    options =>
                        options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            ConfigureCorsServices(services);

            ConfigureSwaggerServices(services);

            ConfigureAuthenticationServices(services, Configuration["AuthenticationSettings:Issuer"], Configuration["AuthenticationSettings:Audience"], Configuration["AuthenticationSettings:SigningKey"]);
        }

        public void ConfigureCorsServices(IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    );
                }
            );
        }

        public void ConfigureSwaggerServices(IServiceCollection services)
        {
            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.DocumentName = "swagger";
                // Add operation security scope processor
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                // Add custom document processors, etc.
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT token", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Type into the textbox: Bearer {your JWT token}.",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));
                // Post process the generated document
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Description = "Nojomo Core Web API";
                    document.Schemes = new[] { OpenApiSchema.Https, OpenApiSchema.Http };
                };
            })
            .AddOpenApiDocument(document => document.DocumentName = "openapi");
        }

        protected void ConfigureAuthenticationServices(IServiceCollection services, string issuer, string audience, string signingKey)
        {
            AuthService.Issuer = issuer;
            AuthService.Audience = audience;
            AuthService.SigningKey = signingKey;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                o =>
                {
                    o.Audience = AuthService.Audience;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthService.Issuer,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(AuthService.SigningKey)
                        )
                    };
                }
            );

            services.AddAuthorization(
                options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme
                    )
                    .RequireAuthenticatedUser()
                    .Build();
                }
            );

            services.AddSingleton<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi(
                a => {
                    a.PostProcess = (document, _) => {
                        document.Schemes = new[] { OpenApiSchema.Https, OpenApiSchema.Http };
                    };
                }
            );
            app.UseSwaggerUi3();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

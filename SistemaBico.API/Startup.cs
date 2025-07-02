using EFCoreSecondLevelCacheInterceptor;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sistema.Bico.Domain.AutoMapper;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Result;
using Sistema.Bico.Domain.Integration;
using Sistema.Bico.Domain.Integration.Interfaces;
using Sistema.Bico.Domain.Interface.Services;
using Sistema.Bico.Domain.Service;
using Sistema.Bico.Domain.UseCases;
using Sistema.Bico.Infra.Context;
using SistemaBico.API.Configurations;
using System.Globalization;
using System.Reflection;

namespace Sistema.Bico.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Console.WriteLine(" Iniciando Startup...");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("🔧 Configurando Services...");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddMemoryCache();
            services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider()
                       .ConfigureLogging(true)
                       .UseCacheKeyPrefix("EF_")
                       .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(2)));

            services.AddDbContext<ContextBase>((serviceProvider, options) =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                       .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()));

            services.AddHttpClient<IFirebaseNotificationService, FirebaseNotificationService>();
            services.AddScoped<IFirebaseNotificationService, FirebaseNotificationService>();
            services.AddScoped<INotificacoesService, NotificacoesService>();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ContextBase>();

            services.AddInjectRepositorys();
            services.AddInjectHandlers();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<Domain.Generics.Interfaces.INotification, Domain.Generics.Notification.Notification>();
            services.AddScoped<IMercadoPagoIntegration, MercadoPagoIntegration>();
            services.AddScoped<ISmtpEmailComunication, SmtpEmailComunication>();
            services.AddScoped<Domain.Generics.Interfaces.IResult, Result>();

            services.AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddFluentValidation();

            services.AddInjectValidation();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bico API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(option =>
                     {
                         option.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = false,
                             ValidateAudience = false,
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,
                             ValidIssuer = "Bico.Securiry.Bearer",
                             ValidAudience = "Bico.Securiry.Bearer",
                             IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-AESKPERSK2816762")
                         };

                         option.Events = new JwtBearerEvents
                         {
                             OnAuthenticationFailed = context =>
                             {
                                 Console.WriteLine(" Auth Failed: " + context.Exception.Message);
                                 return Task.CompletedTask;
                             },
                             OnTokenValidated = context =>
                             {
                                 Console.WriteLine("Token Validated: " + context.SecurityToken);
                                 return Task.CompletedTask;
                             }
                         };
                     });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("🚦 Executando Configure...");

            try
            {
                app.UseMiddleware<ExceptionHandlingMiddleware>();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseStaticFiles();
                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                // Swagger padrão
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bico API v1");
                    c.RoutePrefix = "swagger";
                });

                // Endpoint padrão e health
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();

                    // Endpoint básico de health check
                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("API rodando!");
                    });

                    endpoints.MapGet("/health", async context =>
                    {
                        await context.Response.WriteAsync("Healthy");
                    });
                });

                Console.WriteLine(" Aplicação inicializada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Erro na configuração do pipeline: {ex.Message}");
                throw;
            }
        }
    }
}

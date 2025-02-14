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
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // *** ESTE BLOCO PRECISA INSERIR
            //services.AddLettuceEncrypt();
            // FIM DO BLOCO

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        

            // Inject Hosted
            //services.AddHostedService<QueueConsumerRegisterClient>();
            //services.AddHostedService<QueueConsumerRegisterWorker>();
            //services.AddHostedService<QueueConsumerApplyWorker>();
            //services.AddHostedService<QueueConsumerApplyProfessional>();
            //services.AddHostedService<QueueConsumerRegisterProfessional>();
            //services.AddHostedService<QueueConsumerCancelApplyProfessional>();
            //services.AddHostedService<QueueConsumerDoneWorker>();
            //services.AddHostedService<QueueConsumerPayment>();
            //services.AddHostedService<QueueConsumerWorkerCancelPlan>();
            //services.AddHostedService<QueueConsumerSendEmail>();
            //services.AddHostedService<QueueConsumerForgotClient>();


            services.AddMemoryCache();
            services.AddEFSecondLevelCache(options =>
                options.UseMemoryCacheProvider().ConfigureLogging(true).UseCacheKeyPrefix("EF_")
                        // Fallback on db if the caching provider fails.
                        .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(2)));

            services.AddDbContext<ContextBase>((serviceProvider, options) =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                  .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()));

            services.AddHttpClient<IFirebaseNotificationService, FirebaseNotificationService>();

            // Adicionando a injeção da interface para a implementação
            services.AddScoped<IFirebaseNotificationService, FirebaseNotificationService>();
            services.AddScoped<INotificacoesService, NotificacoesService>();


            services.AddDefaultIdentity<ApplicationUser>(options => 
            { 
              options.SignIn.RequireConfirmedAccount = false;
              options.SignIn.RequireConfirmedPhoneNumber = false;

              options.Password.RequireDigit = false;
              options.Password.RequireLowercase = false;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireUppercase = false;
              options.Password.RequiredLength = 6;
              options.Password.RequiredUniqueChars = 0;

            }).AddEntityFrameworkStores<ContextBase>();

            // Repositorys
            services.AddInjectRepositorys();

            // Commands
            services.AddInjectHandlers();

          //  services.AddTransient<IRequestHandler<QueuePublishWorkerCancelPlanCommand, Unit>, QueuePublishWorkerCancelPlanCommandHandler>();
            services.AddHttpClient();
            //services.AddHostedService<WorkerCancelPlansExpiration>();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            // Notification
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped<Domain.Generics.Interfaces.INotification, Domain.Generics.Notification.Notification>();
            services.AddScoped<IMercadoPagoIntegration, MercadoPagoIntegration>();
            services.AddScoped<ISmtpEmailComunication, SmtpEmailComunication>();
            services.AddScoped<Domain.Generics.Interfaces.IResult, Result>();

            services.AddControllers()
                .AddNewtonsoftJson(s =>
                {
                    s.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                }).AddFluentValidation();

            services.AddInjectValidation();

            services.AddControllersWithViews();
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
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

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });

            });


           // services.AddSignalR();


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
                                 Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                 return Task.CompletedTask;
                             },
                             OnTokenValidated = context =>
                             {
                                 Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                 return Task.CompletedTask;
                             }
                         };
                     });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder => builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader());
            //});


            services.AddSwaggerGenNewtonsoftSupport();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               // endpoints.MapHub<PaymentHub>("/signalr-hub");
            });

            app.UseSwaggerESwaggerUI();
        }
    }
}

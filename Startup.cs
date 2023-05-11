using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Business.ServiceExtensions;
using Common.Entites;

namespace SampleStructure
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddResponseCompression(options =>
            //{
            //    options.EnableForHttps = true;
            //});

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           // services.Configure<AuthConfig>((Configuration.GetSection("IdentityServerSettings")));

            //services.Configure<StoragePocConfig>((Configuration.GetSection("StoragePOCServerSettings")));

            //var authConfig = Configuration.GetSection("IdentityServerSettings").Get<object>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "localhost";

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false
                };
                options.RequireHttpsMetadata = false;
            });

      
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            string recaptchaSecretKey = Configuration.GetValue<string>("ReCaptcha:secretKey");
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IReCaptchaService, ReCaptchaService>();
            //services.AddScoped<IReportService, ReportService>();
            //services.AddScoped<IAuthApiService, AuthApiService>();
            //services.AddScoped<ICvrApiService, CvrApiService>();

            //services.Configure<AuthConfig>((Configuration.GetSection("NetsConfiguration")));
            //services.AddScoped<INetsService, NetsService>();

            //services.AddScoped<IStoragePocService, StoragePocService>();

            services.AddBusinessService();
            services.AddOData();

            services.AddCors(options =>
            {
                options.AddPolicy("AllCors",
                                  builder =>
                                  {
                                      builder
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                  });
            });
        }

        public Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment { get; set; }
        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment) => _hostingEnvironment = hostingEnvironment;
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //  app.UseResponseCompression();

           // loggerFactory.AddProvider(new LoggerProvider(_hostingEnvironment));

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                    context.Response.Headers["Access-Control-Allow-Credentials"] = "true";

                    return Task.CompletedTask;
                });

                await next();
            });

            app.UseODataBatching();
            app.UseRouting();

            // NOTE: Cors ayarları her zaman routingden sonra gelmeli yeri değiştirilmemeli.
            app.UseCors("AllCors");
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"UploadedFiles")),
                RequestPath = new PathString("/UploadedFiles")
            });

           // app.UseMiddleware<ExceptionLogMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                var defaultBatchHandler = new DefaultODataBatchHandler();
                defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;

                endpoints.Expand().Select().Filter().OrderBy().Count().MaxTop(null);
                endpoints.MapODataRoute(
                    routeName: "bogforingapi",
                    routePrefix: "bogforingapi",
                    model: GetEdmModel(),
                    batchHandler: defaultBatchHandler
                    );

                //endpoints.MapControllers().RequireAuthorization("ApiScope");
            });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();

            builder.EntitySet<Company>("Company");
          

            return builder.GetEdmModel();
        }
    }
}

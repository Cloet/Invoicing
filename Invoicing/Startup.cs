using Autofac;
using Invoicing.Base.Logging;
using Invoicing.Helpers;
using LogLevel = Invoicing.Base.Logging.LogLevel;

namespace Invoicing
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IConfigurationRoot configuration)
        {
            Settings();
            Configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacMainModule());
            builder.RegisterModule(new AutofacRepositoryModule());
            builder.RegisterModule(new AutofacServiceModule());
        }

        private void Settings()
        {
            var settings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional: false, reloadOnChange: false)
                .Build();
            InitLoggerSettings(settings);
        }

        private void InitLoggerSettings(IConfigurationRoot settings)
        {
            var config = new LoggerConfig
            {
                BaseDir = settings.GetSection("Logger:BaseDir").Value
            };

            if (bool.TryParse(settings.GetSection("Logger:UseSubDirs").Value, out var subdirs))
                config.SeparateSubDirectories = subdirs;
            if (int.TryParse(settings.GetSection("Logger:LogLevel").Value, out var level))
                config.LoggerLevel = (LogLevel)level;
            else
                config.LoggerLevel = LogLevel.All;
            if (bool.TryParse(settings.GetSection("Logger:ExtendedLogging").Value, out var extended))
                config.ExtendedLogging = extended;
            if (bool.TryParse(settings.GetSection("Logger:SeparateLogLevelFiles").Value, out var separate))
                config.SeparateLogLevelFiles = separate;

            LogManager.InitializeLogManager(config);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(WebApplication app, IHostApplicationLifetime lifetime)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html"); ;
        }


    }
}

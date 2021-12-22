using Autofac;
using Autofac.Extensions.DependencyInjection;
using Invoicing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var startup = new Startup(builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Startup items & autofac.
startup.ConfigureServices(builder.Services);
builder.Host.ConfigureContainer<ContainerBuilder>(builder => startup.ConfigureContainer(builder));


var app = builder.Build();

startup.Configure(app, app.Lifetime);

app.Run();

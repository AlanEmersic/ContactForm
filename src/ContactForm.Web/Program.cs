using ContactForm.Application;
using ContactForm.Domain;
using ContactForm.Infrastructure;
using ContactForm.Web.Components;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication()
    .AddInfrastructure();

WebApplication app = builder.Build();

app.UseInfrastructure();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
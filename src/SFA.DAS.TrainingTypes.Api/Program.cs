using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using SFA.DAS.Api.Common.AppStart;
using SFA.DAS.Api.Common.Configuration;
using SFA.DAS.Api.Common.Infrastructure;
using SFA.DAS.TrainingTypes.Api.AppStart;
using SFA.DAS.TrainingTypes.Api.Infrastructure;
using SFA.DAS.TrainingTypes.Domain.Configuration;

var builder = WebApplication.CreateBuilder(args);

var rootConfiguration = builder.Configuration.LoadConfiguration();

builder.Services.AddOptions();
builder.Services.Configure<TrainingTypeConfiguration>(rootConfiguration.GetSection(nameof(TrainingTypeConfiguration)));
builder.Services.AddSingleton(cfg => cfg.GetService<IOptions<TrainingTypeConfiguration>>()!.Value);

builder.Services.AddServiceRegistration();

var candidateAccountConfiguration = rootConfiguration
    .GetSection(nameof(TrainingTypeConfiguration))
    .Get<TrainingTypeConfiguration>();

if (rootConfiguration["EnvironmentName"] != "DEV")
{
    builder.Services.AddHealthChecks();

}

if (!(rootConfiguration["EnvironmentName"]!.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
      rootConfiguration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)))
{
    var azureAdConfiguration = rootConfiguration
        .GetSection("AzureAd")
        .Get<AzureActiveDirectoryConfiguration>();

    var policies = new Dictionary<string, string>
    {
        {PolicyNames.Default, RoleNames.Default},
    };
    builder.Services.AddAuthentication(azureAdConfiguration, policies);
}

builder.Services
    .AddMvc(o =>
    {
        if (!(rootConfiguration["EnvironmentName"]!.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
              rootConfiguration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)))
        {
            o.Conventions.Add(new AuthorizeControllerModelConvention(new List<string> ()));
        }
        o.Conventions.Add(new ApiExplorerGroupPerVersionConvention());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrainingTypesApi", Version = "v1" });
    c.OperationFilter<SwaggerVersionHeaderFilter>();
    c.DocumentFilter<JsonPatchDocumentFilter>();
});
            
builder.Services.AddApiVersioning(opt => {
    opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrainingTypesApi v1");
    c.RoutePrefix = string.Empty;
});
            
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();

if (!app.Configuration["EnvironmentName"]!.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
{
    app.UseHealthChecks();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "api/{controller=Users}/{action=Index}/{id?}");
app.Run();
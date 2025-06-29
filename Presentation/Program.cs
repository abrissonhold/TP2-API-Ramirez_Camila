using Application.Exceptions;
using Application.Interfaces;
using Application.UserCase;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Examples;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Solicitud de Proyecto",
        Description = "API para la gestión y aprobación de solicitudes de proyectos"
    });
    options.ExampleFilters();
    string xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<ApiErrorExample>();

string? connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt =>
                            opt.UseSqlServer(connectionString));

builder.Services.AddTransient<IProjectProposalService, ProjectProposalService>();
builder.Services.AddTransient<IProjectProposalCommand, ProjectProposalCommand>();
builder.Services.AddTransient<IProjectProposalQuery, ProjectProposalQuery>();

builder.Services.AddTransient<IProjectApprovalStepService, ProjectApprovalStepService>();
builder.Services.AddTransient<IProjectApprovalStepCommand, ProjectApprovalStepCommand>();
builder.Services.AddTransient<IProjectApprovalStepQuery, ProjectApprovalStepQuery>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserQuery, UserQuery>();

builder.Services.AddTransient<IApprovalRuleQuery, ApprovalRuleQuery>();

builder.Services.AddTransient<IAreaService, AreaService>();
builder.Services.AddTransient<IAreaQuery, AreaQuery>();

builder.Services.AddTransient<IProjectTypeService, ProjectTypeService>();
builder.Services.AddTransient<IProjectTypeQuery, ProjectTypeQuery>();

builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IRoleQuery, RoleQuery>();

builder.Services.AddTransient<IApprovalStatusService, ApprovalStatusService>();
builder.Services.AddTransient<IApprovalStatusQuery, ApprovalStatusQuery>();

builder.Services.AddTransient<SeedService>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.ProjectProposal.Any())
    {
        await seedService.SeedProyectosAsync();
    }
}
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        var result = JsonSerializer.Serialize(new ApiError
        {
            message = "Error inesperado: " + error?.Message
        });

        await context.Response.WriteAsync(result);
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.Run();



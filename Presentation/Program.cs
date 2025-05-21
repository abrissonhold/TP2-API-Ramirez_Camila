using Application.Interfaces;
using Application.UserCase;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

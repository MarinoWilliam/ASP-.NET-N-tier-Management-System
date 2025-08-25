using Microsoft.EntityFrameworkCore;
using NTier.ManagementSystem.Data;
using NTier.ManagementSystem.Data.Factory;
using NTier.ManagementSystem.Service.Implementations;
using NTier.ManagementSystem.Service.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                ti =>
                {
                    if (ti.Type == typeof(NTier.ManagementSystem.Data.Entities.Employee))
                    {
                        ti.PolymorphismOptions = new JsonPolymorphismOptions
                        {
                            TypeDiscriminatorPropertyName = "$type",
                            IgnoreUnrecognizedTypeDiscriminators = true,
                            DerivedTypes =
                            {
                                new JsonDerivedType(typeof(NTier.ManagementSystem.Data.Entities.FullTimeEmployee), "fulltime"),
                                new JsonDerivedType(typeof(NTier.ManagementSystem.Data.Entities.FreelancerEmployee), "freelancer")
                            }
                        };
                    }
                }
            }
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDevClient");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

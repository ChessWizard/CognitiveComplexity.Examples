using Core.Repositories;
using Core.UnitofWork;
using Data.Context;
using Data.Repositories;
using Data.Seeds;
using Data.UnitofWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.CQRS.Commands.RegisterUser;
using Service.CQRS.Commands.RegisterUserResolved;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CognitiveComplexityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"), dbOptions =>
    {
        dbOptions.MigrationsAssembly("Data");
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddMediatR(cfg => 
{
    // register any command
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserComplexCommand).Assembly);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<CognitiveComplexityDbContext>();
        await RoleSeeds.AddSeedRoles(dbContext);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "DbContext baþlatma hatasý");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

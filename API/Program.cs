using API.Extensions;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastracture.Data;
using Infrastracture.Identity;
using Infrastracture.Repositories;
using Infrastracture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<SeedData>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
builder.Services.AddScoped<ITaskRepository,TaskRepository>();
builder.Services.AddScoped<ISubcontractorRepository, SubcontractorRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IOfferRepository,OfferRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<AppIdentityDbContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString(("IdentityConnection"))));
builder.Services.AddIdentityCore<AppUser>(opt =>
{

})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddSignInManager<SignInManager<AppUser>>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters() {

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();
var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    SeedData(app);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<AppDbContext>();
var identityContext= services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}
app.Run();
static void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedData>();
        service.Seed();
    }
}
using DEBUG.BL;
using DEBUG.DAL;
using DEBUG.DAL.Context;
using DEBUG.Core.Models;
using Microsoft.AspNetCore.Identity;
using DEBUG.API;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.ConfigureSwaggerAuthentication();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddServer(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;//

    opt.Password.RequiredUniqueChars = 3;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;

    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 3;

}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders(); ;
builder.Services.AddMapperProfiles();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.EnablePersistAuthorization();
        opt.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.AddSeedData();
app.MapControllers();
app.Run();

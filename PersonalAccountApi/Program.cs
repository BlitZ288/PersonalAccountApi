using Microsoft.Extensions.FileProviders;
using PersonalAccount.Domain.Core.Context;
using PersonalAccountApi.Services.UserService;
using PersonalAccountApi.Services.UserService.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
   options =>
   {
       // this defines a CORS policy called "default"
       options.AddPolicy("default", policy =>
       {
           policy.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
       });
   });

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PersonalContext>();


builder.Services.AddTransient<IUserService, UserServise>();

var app = builder.Build();

app.UseCors("default");

if (app.Environment.IsDevelopment())
{

}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

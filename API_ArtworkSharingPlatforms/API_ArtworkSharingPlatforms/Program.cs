using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatforms;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Local DB
builder.Services.AddDbContext<Artwork_SharingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Artwork_Sharing"));
});

builder.Services.AddApiWebService();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("app-cors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

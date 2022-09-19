using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StronglyTypedIdFastEndpoint;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookDbContext>(x => x.UseSqlite("Data Source=test.db"));
builder.Services.AddFastEndpoints();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddSwaggerDoc(settings =>
{
    settings.DocumentName = "swagger";
    settings.Title = "Book API";
    settings.Version = "1.0";
}, serializerSettings: options =>
{
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.Converters.Add(new JsonStringEnumConverter());
}, addJWTBearerAuth: false,
    shortSchemaNames: true,
    removeEmptySchemas: true
);

var app = builder.Build();
using (var dbc = app.Services.CreateScope().ServiceProvider.GetRequiredService<BookDbContext>()){
    dbc.Database.MigrateAsync();
}


app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3();
app.Run();

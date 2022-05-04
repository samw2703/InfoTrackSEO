using System.Net;
using InfoTrackSEO.Api.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGoogleResultsHtmlProvider, GoogleResultsHtmlProvider>();
builder.Services.AddSingleton<IGoogleResultsHtmlParser, GoogleResultsHtmlParser>();
builder.Services.AddSingleton<ISEOResultsProvider, SEOResultsProvider>();
var webClient = new WebClient();
webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
builder.Services.AddSingleton(webClient);

const string corsPolicy = "AllowAny";
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(corsPolicy, policy =>
    {
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);

app.MapControllers();

app.Run();
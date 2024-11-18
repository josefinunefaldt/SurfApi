using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// string ApiKey = "ee7c013a-95fb-11ee-8b92-0242ac130002-ee7c01b2-95fb-11ee-8b92-0242ac130002";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseCors(policy =>
    {
        policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();  //set the allowed origin
    });


    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

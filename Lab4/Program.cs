using Microsoft.EntityFrameworkCore;
using Lab4.DB;
using Lab4.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LrContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("LR4"));
});


builder.Services.AddTransient<IWordRepository, WordRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
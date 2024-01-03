using Microsoft.EntityFrameworkCore;
using WebApplication1.Configuration;
using WebApplication1.DB;
using WebApplication1.Repositories;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WAContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("LR4"));
});

var ms = new MySection();

 builder.Configuration.GetSection(MySection.Key).Bind(ms);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAdd, AddC>();
builder.Services.AddTransient<ISum, Sum>();

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
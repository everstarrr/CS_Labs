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

//var ms = new MySection();

//builder.Configuration.GetSection(MySection.Key).Bind(ms);

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
        
        
        
        
        
        /*Console.WriteLine("Выберите способ сохранения/загрузки данных:\n1. SQLite\n2. XML\n3. JSON");
        var input = Console.ReadLine() ?? "";
        var filename = input switch
        {
            "1" => "dictionary.db",
            "2" => "dictionary.xml",
            "3" => "dictionary.json",
            _ => throw new Exception("Введено неверное значение.")
        };

        var database = new DatabaseConnection(filename);
        var dictionaryDatabase = new WordDictionary(database);
        var appController = new ApplicationController(dictionaryDatabase);
        appController.Run();
        database.SaveDictionary(dictionaryDatabase);
    }
}*/
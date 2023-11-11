using Lab3.Model;
using Lab3.Controller;
using Lab3.Model.Database;

namespace Lab3;
public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Выберите способ сохранения/загрузки данных:\n1. SQLite\n2. XML\n3. JSON");
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
}

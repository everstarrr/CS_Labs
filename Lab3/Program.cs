using Lab3.Model;
using Lab3.Controller;
using Lab3.Model.Database;

namespace Lab3;
public static class Program
{
    public static void Main(string[] args)
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dictionary.db");
        DatabaseConnection database = new DatabaseConnection(path);
        WordDictionary dictionaryDatabase = new WordDictionary(database);
        ApplicationController appController = new ApplicationController(dictionaryDatabase);
        appController.Run();
        database.SaveDictionary(dictionaryDatabase);
    }
}

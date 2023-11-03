using Lab4.Model;
using Lab4.Controller;
using Lab4.Model.Database;

namespace Lab4;
public static class Program
{
    public static void Main(string[] args)
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dictionary.db");
        WordDictionary dictionaryDatabase = new WordDictionary();
        IWordDictionary wordDictionary = dictionaryDatabase;
        DatabaseConnection database = new DatabaseConnection(dictionaryDatabase, path);
        dictionaryDatabase.Dictionary.AddRange(database.LoadDictionary());
        ApplicationController appController = new ApplicationController(wordDictionary);
        appController.Run();
        database.SaveDictionary();
    }
}

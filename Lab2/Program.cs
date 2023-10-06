using Lab2.Model;
using Lab2.Controller;

namespace Lab2;
public class Program
{
    public static void Main(string[] args)
    {
        DictionaryDatabase dictionaryDatabase = new DictionaryDatabase();
        IDictionary dictionary = dictionaryDatabase;
        ApplicationController appController = new ApplicationController(dictionary);
        appController.Run();
    }
}

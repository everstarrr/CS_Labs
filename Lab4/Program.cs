using Lab4.Model;
using Lab4.Controller;

namespace Lab4;
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

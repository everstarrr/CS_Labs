using Lab3.Model;
namespace Lab3.Controller;

//контроллер приложения
public class ApplicationController
{
    private readonly IWordDictionary _wordDictionary; // словарь, реализующий интерфейс IDictionary
    public ApplicationController(IWordDictionary wordDictionary) // конструктор
    {
        _wordDictionary = wordDictionary;
    }

    public void Run() // запуск контроллера
    {
        Console.WriteLine("Для выхода из программы введите символ 'q'.");
        while (true)
        {
            Console.Write("Введите слово: ");
            var input = Console.ReadLine() ?? "";
            if (input.ToLower() == "q")
                break;

            var relatedWords = _wordDictionary.FindRelatedWords(input);
            if (relatedWords.Count > 0)
            {
                Console.WriteLine("Однокоренные слова:");
                foreach (var word in relatedWords)
                {
                    Console.WriteLine(word);
                }
            }
            else
            {
                Console.WriteLine($"Слово '{input}' не найдено в словаре.");
                Console.Write("Хотите добавить его в словарь? (y/n): ");
                var choice = Console.ReadLine() ?? "";
                if (choice.ToLower() == "y")
                {
                    while (true)
                    {
                        string construction = "";
                        Console.Write("Введите приставку: ");
                        string prefix = Console.ReadLine() ?? "";
                        if (prefix != "")
                            construction += prefix + "-";

                        Console.Write("Введите корень слова: ");
                        string root = Console.ReadLine() ?? "";
                        if (root == "")
                            throw new Exception("У слова не может не быть корня.");
                        construction += root;

                        string suf;
                        while (true)
                        {
                            Console.Write("Введите суффикс или окончание: ");
                            suf = Console.ReadLine() ?? "";
                            if (suf == "")
                                break;
                            construction += "-" + suf;
                        }

                        if (input == construction.Replace("-", ""))
                        {
                            _wordDictionary.AddWord(input, construction, root);
                            Console.WriteLine($"Слово {construction} добавлено");
                            break;
                        }

                        Console.WriteLine("Слово и его состав не совпадают.");
                    }
                }
                else if (choice != "n")
                    throw new Exception("Введён неизвестный символ.");
            }
        }
    }
}
using Lab2.Model;
namespace Lab2.Controller;

//контроллер приложения
public class ApplicationController
{
    private readonly IDictionary _dictionary; // словарь, реализующий интерфейс IDictionary

    public ApplicationController(IDictionary dictionary) // конструктор
    {
        _dictionary = dictionary;
    }

    public void Run() // запуск контроллера
    {
        Console.WriteLine("Для выхода из программы введите символ 'q'.");
        while (true)
        {
            Console.Write("Введите слово: ");
            string input = Console.ReadLine() ?? "";
            if (input == "")
                throw new Exception("Пустая строка недопустима.");

            if (input.ToLower() == "q")
                break;

            List<string> relatedWords = _dictionary.FindRelatedWords(input);
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
                string choice = Console.ReadLine() ?? "";
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
                            _dictionary.AddWord(input, construction, root);
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

        _dictionary.SaveDictionary();
    }
}
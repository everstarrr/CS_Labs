using Lab2.Presenter;

namespace Lab2.View;

public class View : Dict
{
    // ввод слова

    public string GetWord()
    {
        Console.Write("> ");
        return Console.ReadLine() ?? throw new NullReferenceException("Пустая строка недопустима.");
    }

    // проверка правильности ввода конструкции
    public bool CheckWord(string word, string construct)
    {
        while (construct.IndexOf('-') != -1)
            construct = construct.Replace("-", "");

        if (construct == word)
            return true;
        return false;
    }

    // проверка наличия слова в словаре
    public char CheckWordInDictionary(string word)
    {
        char c;
        if (!Words.ContainsKey(word)) // если слова нет в словаре
        {
            Console.WriteLine("Неизвестное слово. Хотите добавить его в словарь (y/n)? ");
            char s = Convert.ToChar(Console.ReadLine() ?? "");
            if (char.ToLower(s) != 'n' && char.ToLower(s) != 'y')
                throw new Exception("Ввод иных значений недопустим.");
            c = s;
        }
        else // если слово есть в словаре
        {
            foreach (var str in Elements)
            {
                if (str.Key.Contains(Words[word][1]))
                    Console.WriteLine(Words[str.Key][0]);
            }

            return 'n';
        }

        return c;
    }

    // получение конструкции слова
    public void GetConstruct(string word)
    {
        string core = ""; // корень слова
        int amount = 0; // количество элементов в слове
        string construct = ""; // конструкция слова
        bool checker = false; // чекер правильности ввода конструкции
        while (!checker)
        {
            construct = "";
            string str; // дополнительная переменная

            // добавление приставки
            Console.Write("приставка: ");
            str = Console.ReadLine() ?? "";
            if (str != "")
            {
                amount++;
                construct += str + "-";
            }

            // добавление корня
            Console.Write("корень: ");
            str = Console.ReadLine() ?? "";
            if (str != "")
            {
                core = str;
                amount++;
                construct += str;
            }
            else
            {
                Console.WriteLine("У слова не может не быть корня.");
                continue;
            }

            // добавление суффикса или окончания
            while (true)
            {
                Console.Write("суффикс или окончание: ");
                str = Console.ReadLine() ?? "";
                if (str == "")
                    break;
                amount++;
                construct += "-" + str;
            }

            // проверка правильности ввода
            checker = CheckWord(word, construct);
            if (!checker)
                Console.WriteLine("Конструкция введена неправильно.");
        }

        AddElement(word, amount); // добавление значений в elements
        AddWord(word, construct, core); // добавление слова и корня в Words
    }

    // старт
    public void Start()
    {
        while (true)
        {
            string word = GetWord();
            if (word == "q")
                break;
            if (CheckWordInDictionary(word) == 'y')
                GetConstruct(word);
        }
    }
}
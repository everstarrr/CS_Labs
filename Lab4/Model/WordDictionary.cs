/*using Lab4.Model.Database;

namespace Lab4.Model;

// Реализация словаря
public class WordDictionary : IWordDictionary
{
    public readonly List<WordModel> Dictionary = new(); // список слов и конструкций

    public WordDictionary(IDatabaseConnection db)
    {
        Dictionary.AddRange(db.LoadDictionary());
    }

    public void AddWord(string word, string construction, string root) // добавить слово в словарь
    {
        Dictionary.Add(new WordModel
        {
            Word = word,
            Construction = construction,
            Root = root
        });
    }

    public List<string> FindRelatedWords(string word) // возвращает список с частями слова
    {
        if (word == "")
            throw new Exception("Пустая строка недопустима.");
        List<string> relatedWords = new List<string>();
        string root = "";
        foreach (var wordModel in Dictionary)
        {
            if (wordModel.Word == word)
            {
                root = wordModel.Root ?? throw new Exception("У слова нет корня.");
                break;
            }
        }

        foreach (var wordModel in Dictionary)
        {
            if (wordModel.Root == root)
            {
                relatedWords.Add(wordModel.Construction ?? "");
            }
        }

        return relatedWords.OrderBy(s => s.Count(c => c == '-')).Reverse().ToList();
    }
}*/
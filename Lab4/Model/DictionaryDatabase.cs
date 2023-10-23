using MongoDB.Bson;

namespace Lab4.Model;

using MongoDB.Driver;

// Реализация словаря
public class DictionaryDatabase : IDictionary
{
    static readonly MongoClient Client = new("mongodb://localhost:27017"); //пул подключений к серверу
    private readonly IMongoDatabase _database = Client.GetDatabase("Lab4Base"); //база данных
    private readonly List<WordModel> _dictionary; // список слов и конструкций

    public DictionaryDatabase() // конструктор
    {
        _dictionary = new List<WordModel>();
        LoadDictionary();
    }

    public void AddWord(string word, string construction, string root) // добавить слово в словарь
    {
        _dictionary.Add(new WordModel
        {
            Word = word,
            Construction = construction,
            Root = root
        });

        SaveDictionary();
    }

    public List<string> FindRelatedWords(string word) // возвращает список с частями слова
    {
        List<string> relatedWords = new List<string>();
        string root = "";
        foreach (var wordModel in _dictionary)
        {
            if (wordModel.Word == word)
            {
                root = wordModel.Root ?? throw new Exception("У слова нет корня.");
                break;
            }
        }

        foreach (var wordModel in _dictionary)
        {
            if (wordModel.Root == root)
            {
                relatedWords.Add(wordModel.Construction ?? "");
            }
        }

        return relatedWords.OrderBy(s => s.Count(c => c == '-')).Reverse().ToList();
    }

    public void SaveDictionary() // записывает словарь в файл
    {
        var dict = _database.GetCollection<BsonDocument>("dict");
        
        foreach (var word in _dictionary)
        {
            BsonDocument doc = new BsonDocument
            {
                {"Word", word.Word},
                {"Construction", word.Construction},
                {"Root", word.Root}

            };
            dict.InsertOneAsync(doc);
        }
    }

    private void LoadDictionary() // загружает словарь из файла
    {
        var dict = _database.GetCollection<BsonDocument>("dict");
        List<BsonDocument> users = dict.Find(new BsonDocument()).ToList();
        foreach (var elem in users)
        {
            _dictionary.Add(new WordModel
            {
                Word = elem["Word"].ToString(),
                Construction = elem["Construction"].ToString(),
                Root = elem["Root"].ToString()
            });
        }
    }
}
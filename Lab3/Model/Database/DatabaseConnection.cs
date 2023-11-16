using System.Data.SQLite;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Lab3.Model.Database;

public class DatabaseConnection : IDatabaseConnection
{
    private readonly string _dictionaryFilePath;
    private const string TableName = "DictionaryTable"; // имя таблицы

    public DatabaseConnection(string filename) // конструктор
    {
        _dictionaryFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), filename);
        if (File.Exists(_dictionaryFilePath) || filename != "dictionary.db") return;
        SQLiteConnection.CreateFile(_dictionaryFilePath);
        using var connection = new SQLiteConnection($"Data Source={_dictionaryFilePath};Version=3;");
        connection.Open();
        using var command =
            new SQLiteCommand(
                $"CREATE TABLE IF NOT EXISTS {TableName} (Word TEXT, Construction TEXT, Root TEXT);",
                connection);
        command.ExecuteNonQuery();
    }


    public void SaveDictionary(WordDictionary dictionary)
    {
        if (_dictionaryFilePath.Contains("dictionary.db")) // SQLite
        {
            using var connection = new SQLiteConnection($"Data Source={_dictionaryFilePath};Version=3;");
            connection.Open();

            using (var command = new SQLiteCommand($"DELETE FROM {TableName};", connection)) // очищение бд
            {
                command.ExecuteNonQuery();
            }

            foreach (var word in dictionary.Dictionary) // добавление слов из словаря в бд
            {
                using var command =
                    new SQLiteCommand(
                        $"INSERT INTO {TableName} (Word, Construction, Root) VALUES (@Word, @Construction, @Root);",
                        connection);
                command.Parameters.AddWithValue("@Word", word.Word);
                command.Parameters.AddWithValue("@Construction", word.Construction);
                command.Parameters.AddWithValue("@Root", word.Root);
                command.ExecuteNonQuery();
            }
        }
        else if (_dictionaryFilePath.Contains("dictionary.json")) // JSON
        {
            File.WriteAllText(_dictionaryFilePath, string.Empty);
            var jsonData = JsonConvert.SerializeObject(dictionary.Dictionary, Formatting.Indented);
            File.WriteAllText(_dictionaryFilePath, jsonData);
        }
        else if (_dictionaryFilePath.Contains("dictionary.xml")) // XML
        {
            var formatter = new XmlSerializer(typeof(List<WordModel>));
            using var fs = new FileStream(_dictionaryFilePath, FileMode.OpenOrCreate);
            formatter.Serialize(fs, dictionary.Dictionary);
        }
    }

    public IEnumerable<WordModel> LoadDictionary() // загружает словарь из бд
    {
        if (_dictionaryFilePath.Contains("dictionary.db")) // SQLite
        {
            using var connection = new SQLiteConnection($"Data Source={_dictionaryFilePath};Version=3;");
            connection.Open();
            using var command = new SQLiteCommand($"SELECT Word, Construction, Root FROM {TableName};", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var word = reader.GetString(0);
                var construction = reader.GetString(1);
                var root = reader.GetString(2);
                yield return new WordModel
                {
                    Word = word,
                    Construction = construction,
                    Root = root
                };
            }
        }
        else if (_dictionaryFilePath.Contains("dictionary.json")) // JSON
        {
            if (!File.Exists(_dictionaryFilePath))
                File.Create(_dictionaryFilePath).Close();
            else
            {
                var jsonData = File.ReadAllText(_dictionaryFilePath);
                var data = JsonConvert.DeserializeObject<List<WordModel>>(jsonData) ??
                           new List<WordModel>();
                foreach (var word in data)
                {
                    yield return new WordModel
                    {
                        Word = word.Word,
                        Construction = word.Construction,
                        Root = word.Root
                    };
                }
            }
        }
        else if (_dictionaryFilePath.Contains("dictionary.xml")) // XML
        {
            if (!File.Exists(_dictionaryFilePath))
                File.Create(_dictionaryFilePath).Close();
            else
            {
                var formatter = new XmlSerializer(typeof(List<WordModel>));
                using var fs = new FileStream(_dictionaryFilePath, FileMode.OpenOrCreate);
                var allwords = formatter.Deserialize(fs) as List<WordModel>;

                if (allwords == null) yield break;
                foreach (var word in allwords)
                {
                    yield return new WordModel
                    {
                        Word = word.Word,
                        Construction = word.Construction,
                        Root = word.Root
                    };
                }
            }
        }
    }
}
using System.Data.SQLite;

namespace Lab3.Model.Database;

public class DatabaseConnection : IDatabaseConnection
{
    private readonly List<WordModel> _dictionary;
    private readonly string _dictionaryFilePath;
    private readonly string _tableName = "DictionaryTable"; // имя таблицы


    public DatabaseConnection(WordDictionary dictionary, string path)
    {
        _dictionary = dictionary.Dictionary;
        _dictionaryFilePath = path;
        if (!File.Exists(_dictionaryFilePath))
        {
            SQLiteConnection.CreateFile(_dictionaryFilePath);
            using var connection = new SQLiteConnection($"Data Source={_dictionaryFilePath};Version=3;");
            connection.Open();
            using var command =
                new SQLiteCommand(
                    $"CREATE TABLE IF NOT EXISTS {_tableName} (Word TEXT, Construction TEXT, Root TEXT);",
                    connection);
            command.ExecuteNonQuery();
        }
    }


    public void SaveDictionary()
    {
        using var connection = new SQLiteConnection($"Data Source={_dictionaryFilePath};Version=3;");
        connection.Open();

        using (var command = new SQLiteCommand($"DELETE FROM {_tableName};", connection)) // очищение бд
        {
            command.ExecuteNonQuery();
        }

        foreach (var word in _dictionary) // добавление слов из словаря в бд
        {
            using var command =
                new SQLiteCommand(
                    $"INSERT INTO {_tableName} (Word, Construction, Root) VALUES (@Word, @Construction, @Root);",
                    connection);
            command.Parameters.AddWithValue("@Word", word.Word);
            command.Parameters.AddWithValue("@Construction", word.Construction);
            command.Parameters.AddWithValue("@Root", word.Root);
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<WordModel> LoadDictionary() // загружает словарь из бд
    {
        using var connection = new SQLiteConnection($"Data Source={_dictionaryFilePath};Version=3;");
        connection.Open();
        using var command = new SQLiteCommand($"SELECT Word, Construction, Root FROM {_tableName};", connection);
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
}
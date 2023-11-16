namespace Lab3.Model.Database;

public interface IDatabaseConnection
{
    void SaveDictionary(WordDictionary dictionary);
    
    IEnumerable<WordModel> LoadDictionary();
}
namespace Lab3.Model.Database;

public interface IDatabaseConnection
{
    void SaveDictionary();
    
    IEnumerable<WordModel> LoadDictionary();
}
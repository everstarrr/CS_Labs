namespace Lab4.Model.Database;

public interface IDatabaseConnection
{
    void SaveDictionary();
    
    IEnumerable<WordModel> LoadDictionary();
}
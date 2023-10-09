namespace Lab2.Model;

// Интерфейс словаря
public interface IDictionary
{
    void AddWord(string word, string construction, string root);
    List<string> FindRelatedWords(string word);
    void SaveDictionary();
}
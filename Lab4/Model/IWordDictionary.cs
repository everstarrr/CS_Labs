namespace Lab4.Model;

// Интерфейс словаря
public interface IWordDictionary
{
    void AddWord(string word, string construction, string root);
    List<string> FindRelatedWords(string word);
}
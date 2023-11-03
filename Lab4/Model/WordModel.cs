namespace Lab4.Model;

// Модель данных для слова
public class WordModel
{
    public WordModel(string word, string construction, string root)
    {
        Word = word;
        Construction = construction;
        Root = root;
    }
    public WordModel(){}
    public string? Word { get; set; }
    public string? Construction { get; set; }
    public string? Root { get; set; }
}
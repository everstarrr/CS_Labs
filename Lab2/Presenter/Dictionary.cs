namespace Lab2.Presenter;

public class Dict
{
    // словарь слов, конструкций и корней
    protected Dictionary<string, string[]> Words = new();

    // словарь слов и количеств элементов
    protected Dictionary<string, int> Elements = new();

    // добавить слово в словарь
    public void AddWord(string word, string construct, string core)
    {
        Words.Add(word, new []{construct, core});
        Console.WriteLine("Слово “{0}“ добавлено.", word);
    }

    // добавить слово и количество элементов в нем в словарь elements
    public void AddElement(string word, int amount)
    {
        Elements.Add(word, amount);
        Elements = Elements.OrderBy(x => x.Value).ToDictionary(x
            => x.Key, x => x.Value);
    }
    
    
}
using Moq;
using Lab3.Model.Database;
using Lab3.Model;

namespace Lab3.Test;

public class WordDictionaryTests
{
    [Theory]
    [InlineData("братан", "брат-ан", "брат", "братан")]
    [InlineData("человек", "чел-о-век", "чел", "человек")]
    public void AddWord_ShouldWork(string word, string construction, string root, string expected)
    {
        var mock = new Mock<IDatabaseConnection>();
        mock.Setup(r => r.LoadDictionary()).Returns(new List<WordModel>());
        var dict = new WordDictionary(mock.Object);
        dict.AddWord(word, construction, root);

        Assert.Equal(expected, dict.Dictionary[0].Word);
    }
    
    
    [Theory]
    [InlineData ("челик", "чел-ик", "чел","человек", "чел-о-век", "чел")]
    
    public void FindRelatedWords_ReturnsRelatedWords(string word1, string construction1, string root1, 
        string word2, string construction2, string root2)
    {
        var mock = new Mock<IDatabaseConnection>();
        mock.Setup(r => r.LoadDictionary()).Returns(new List<WordModel>());
        var dict = new WordDictionary(mock.Object);

        dict.AddWord(word1, construction1, root1);
        dict.AddWord(word2, construction2, root2);
        var actRelWords = dict.FindRelatedWords("человек");
        var expRelWords = new List<string>();
        expRelWords.Add(construction2);
        expRelWords.Add(construction1);

        Assert.Equal(expRelWords, actRelWords);
    }

    [Fact]
    public void FindRelatedWords_ThrowsException()
    {
        var mock = new Mock<IDatabaseConnection>();                                                     
        mock.Setup(r => r.LoadDictionary()).Returns(new List<WordModel>());                             
        var dict = new WordDictionary(mock.Object);

        Assert.Throws<Exception>(() => dict.FindRelatedWords(""));
    }
}
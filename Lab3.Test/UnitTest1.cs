using Moq;
using Lab3.Model.Database;
using Lab3.Model;

namespace Lab3.Test;

public class UnitTest1
{
    [Fact]
    public void AddWord_ShouldWork()
    {
        var mock = new Mock<IDatabaseConnection>();
        mock.Setup(r => r.LoadDictionary()).Returns(new List<WordModel>());
        WordDictionary dict = new WordDictionary(mock.Object);
        dict.AddWord("челик","чел-ик","чел");
        dict.AddWord("человек","чел-о-век","чел");
        
        Assert.Equal("челик", dict.Dictionary[0].Word);
        Assert.Equal("человек", dict.Dictionary[1].Word);
    }

    [Fact]
    public void FindRelatedWords_ReturnsRelatedWords()
    {
        var mock = new Mock<IDatabaseConnection>();
        mock.Setup(r => r.LoadDictionary()).Returns(new List<WordModel>());
        WordDictionary dict = new WordDictionary(mock.Object);
        
        dict.AddWord("челик","чел-ик","чел");
        dict.AddWord("человек","чел-о-век","чел");
        List<string> actRelWords = dict.FindRelatedWords("человек");
        List<string> expRelWords = new List<string>();
        expRelWords.Add("чел-о-век");
        expRelWords.Add("чел-ик");

        Assert.Equal(expRelWords, actRelWords);
    }
}
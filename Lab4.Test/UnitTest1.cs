using Moq;
using Lab4.Model.Database;
using Lab4.Model;

namespace Lab4.Test;

public class UnitTest1
{
    [Fact]
    public void LoadDictionary_ShouldWork()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "testDictionary.db");
        WordDictionary dict = new WordDictionary();
        var mock = new Mock<IDatabaseConnection>();
        dict.AddWord("челик","чел-ик","чел");
        dict.AddWord("человек","чел-о-век","чел");
        mock.Setup(r => r.LoadDictionary()).Returns(dict.Dictionary.GetRange(0,3));
        IEnumerable<WordModel> actual = mock.Object.LoadDictionary();
        WordModel expected = new WordModel("челик","чел-ик","чел");
        
        Assert.Equal(actual, expected);
    }

    [Fact]
    public void FindRelatedWords_ReturnsRelatedWords()
    {
       
    }
}
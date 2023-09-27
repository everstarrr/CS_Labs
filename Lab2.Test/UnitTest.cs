namespace Lab2.Test;
using View;

public class UnitTest
{
    [Theory]
    [InlineData("братанчик", "брат-ан-чик")]
    public void CheckWord_ShouldBeTrue(string word, string construct)
    {
        var v = new View();
        var checker = v.CheckWord(word, construct);
        Assert.True(checker);
    }
    
    [Theory]
    [InlineData("братанчик", "братан")]
    public void CheckWord_ShouldBeFalse(string word, string construct)
    {
        var v = new View();
        var checker = v.CheckWord(word, construct);
        Assert.False(checker);
    }

    [Theory]
    [InlineData("джавист", "джав-ист", "джав", 2)]
    public void PrintWordsInDictionary(string word, string construct, string core, int amount)
    {
        var v = new View();
        v.AddWord(word,construct,core);
        v.AddElement(word,amount);


        Assert.Equal('n', v.CheckWordInDictionary(word));
    }
}
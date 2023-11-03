using Lab2.Model;

namespace Lab2.Test;

public class UnitTest
{
    [Theory]
    [InlineData("братан", "брат-ан", "брат")]
    [InlineData("человек", "чел-о-век", "чел")]
    public void FindRelatedWords_ShouldReturnExpected(string word, string construction, string root)
    {
        DictionaryDatabase db = new DictionaryDatabase();
        db.AddWord(word, construction, root);
        string result = db.FindRelatedWords(word)[0];

        Assert.Equal(construction, result);
    }

    [Fact]
    public void SaveAndLoadDictionary_ShouldBeSame()
    {
        DictionaryDatabase db = new DictionaryDatabase();
        db.AddWord("кувалда", "кувалд-а", "кувалд");
        db.AddWord("приход", "при-ход", "ход");
        db.AddWord("павапепа", "павапепа", "павапепа");
        DictionaryDatabase dbtest = db;
        dbtest.SaveDictionary();
        dbtest.LoadDictionary();
        
        Assert.Equal(db, dbtest);
    }
    
    
}
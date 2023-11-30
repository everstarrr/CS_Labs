using Lab4.Model;

namespace Lab4.Services;

public class StoreWord : IStoreWord
{
    private readonly IValidation _validator;
    public StoreWord(IValidation validator)
    {
        _validator = validator;
    }
    
    public WordModel SetWordModel(string word, string construction, string root)
    {
        var wordModel = new WordModel
        {
            Word = word,
            Construction = construction,
            Root = root
        };

        _validator.ValidateWordModel(wordModel);
        return wordModel;

    }
}
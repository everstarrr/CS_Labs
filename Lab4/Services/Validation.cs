using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Lab4.Model;

namespace Lab4.Services;

public class Validation : IValidation
{
    public void ValidateWordModel(WordModel wordModel)
    {
        if (wordModel.Word=="" || wordModel.Construction=="" || wordModel.Root=="")
            throw new Exception("Пустые строки недопустмимы.");
        if (wordModel.Word != wordModel.Construction?.Replace("-", ""))
        {
            throw new Exception("Слово и его состав не совпадают.");
        }

        var regex = new Regex($@"{wordModel.Root}(\w*)");
        if (regex.Matches(wordModel.Word ?? "").Count == 0)
            throw new Exception("В слове нет корня");
    }
}
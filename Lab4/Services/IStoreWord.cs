using Lab4.Model;

namespace Lab4.Services;

public interface IStoreWord
{
    WordModel SetWordModel(string word, string construction, string root);
}
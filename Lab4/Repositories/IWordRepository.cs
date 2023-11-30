using Lab4.Model;

namespace Lab4.Repositories;

public interface IWordRepository
{
    Task? Add(WordModel wordModel);
    Task<List<string?>>? GetRelatedWords(string word);
}
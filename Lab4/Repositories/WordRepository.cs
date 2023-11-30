using Lab4.DB;
using Lab4.Model;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Repositories;

public class WordRepository:IWordRepository
{
    private readonly LrContext _context;

    public WordRepository(LrContext context)
    {
        _context = context;
    }

    public Task Add(WordModel wordModel)
    {
        if (!_context.WordModels!.Any(w => w.Word == wordModel.Word && w.Construction == wordModel.Construction
                                                                   && w.Root == wordModel.Root))
        {
            _context.WordModels?.Add(wordModel);
            return _context.SaveChangesAsync();
        }

        throw new Exception("Слово уже есть в словаре.");
    }

    public Task<List<string?>> GetRelatedWords(string word)
    {
        if (_context.WordModels!.Any(w => w.Word == word))
        {
            var wm = _context.WordModels?.FirstOrDefault(w => w.Word == word);
            var root = wm!.Root;
            return  _context.WordModels!.Where(w => w.Root == root)
                .Select(w => w.Construction)
                .ToListAsync();
        }

        throw new Exception("Однокоренных слов нет.");
    }
}
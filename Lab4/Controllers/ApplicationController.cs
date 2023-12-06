using Lab4.Repositories;
using Lab4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers;

//контроллер приложения
[ApiController]
public class ApplicationController : ControllerBase
{

    private readonly IWordRepository _wordRepository;

    public ApplicationController(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }


    [HttpPost]
    [Route("/add-word")]
    public Task? Add([FromBody] string fake, string word, string construction, string root)
    {
        var validator = new Validation();
        var storeWord = new StoreWord(validator);
        var wm = storeWord.SetWordModel(word, construction, root);

        return _wordRepository.Add(wm);
    }
    
    [HttpGet]
    [Route("/get-related-words")]
    public Task<List<string?>>? GetRelatedWords(string word)
    {
        return _wordRepository.GetRelatedWords(word);
    }
}

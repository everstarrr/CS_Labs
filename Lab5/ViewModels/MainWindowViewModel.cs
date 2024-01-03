using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DynamicData;
using Lab5.Database;
using Lab5.Models;
using Lab5.Repository;
using Lab5.Views;
using ReactiveUI;

namespace Lab5.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly MyContext _context;
    private readonly IMyRepository _repository;
    private string _inputWord;
    private bool _isInputValid;
    
    public MainWindowViewModel()
    {
        _context = new MyContext();
        _repository = new MyRepository(_context);
        var t = _repository.LoadFromDb();
        WordList.AddRange(t);
        WordList = new ObservableCollection<WordModel>(_repository.LoadFromDb());
        Nodes = new ObservableCollection<Node>();
    }

    private void ValidateInput()
    {
        IsInputValid = !string.IsNullOrEmpty(InputWord);
    }

    public bool IsInputValid
    {
        get => _isInputValid;
        set => this.RaiseAndSetIfChanged(ref _isInputValid, value);
    }
    
    public string InputWord
    {
        get => _inputWord;
        set
        {
            this.RaiseAndSetIfChanged(ref _inputWord, value); 
            ValidateInput();
        }
    }
    


    public ObservableCollection<WordModel> WordList { get; set; } = [];

    public void ShowAddWordDialog()
    {
        if (_inputWord != null)
        {
            var dialog = new DialogWindow();
            dialog.DataContext = new DialogViewModel(this, dialog, _inputWord);

            dialog.Show();
        }
    }

    

    public async Task AddWord(string word, string construction, string root)
    {
        await Task.Delay(1000);
        var newWord = new WordModel
        {
            Word = word,
            Construction = construction,
            Root = root
        };
        WordList.Add(newWord);
        _repository.SaveToDb(WordList);
    }
    
    public ObservableCollection<Node> Nodes { get; set; }
    
    public void Search()
    {
        Nodes.Clear();
        Nodes.Add(new Node(InputWord, new ObservableCollection<Node>()));
        
        foreach (var wl in WordList)
        {
            if (wl.Root == InputWord)
            {
                Nodes[0].SubNodes.Add(new Node(wl.Construction));
            }
        }
        
    }
}
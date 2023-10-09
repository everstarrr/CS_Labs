namespace Lab2.Model;

 // Реализация словаря
    public class DictionaryDatabase : IDictionary
    {
        private readonly List<WordModel> _dictionary; // список слов и конструкций
        private readonly string _dictionaryFilePath = 
            Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dictionary.txt"); // путь к .txt

        public DictionaryDatabase() // конструктор
        {
            _dictionary = new List<WordModel>();
            LoadDictionary();
        }

        public void AddWord(string word, string construction, string root) // добавить слово в словарь
        {
            _dictionary.Add(new WordModel
            {
                Word = word,
                Construction = construction,
                Root = root
            });

            SaveDictionary();
        }

        public List<string> FindRelatedWords(string word) // возвращает список с частями слова
        {
            List<string> relatedWords = new List<string>();
            string root = "";
            foreach (var wordModel in _dictionary)
            {
                if (wordModel.Word == word)
                {
                    root = wordModel.Root ?? throw new Exception("У слова нет корня.");
                    break;
                }
            }
            foreach (var wordModel in _dictionary)
            {
                if (wordModel.Root == root)
                {
                    relatedWords.Add(wordModel.Construction ?? "");
                }
            }
            return relatedWords.OrderBy(s => s.Count(c => c == '-')).Reverse().ToList();
        }

        public void SaveDictionary() // записывает словарь в файл
        {
            using (StreamWriter writer = new StreamWriter(_dictionaryFilePath))
            {
                foreach (var word in _dictionary)
                {
                    writer.WriteLine($"{word.Word};{word.Construction};{word.Root}");
                }
            }
        }

        public void LoadDictionary() // загружает словарь из файла
        {
            if (File.Exists(_dictionaryFilePath))
            {
                using (StreamReader reader = new StreamReader(_dictionaryFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine() ?? "") != "")
                    {
                        string[] parts = line.Split(';');
                        string word = parts[0];
                        string construction = parts[1];
                        string root = parts[2];
                        _dictionary.Add(new WordModel
                        {
                            Word = word,
                            Construction = construction,
                            Root = root
                        });
                    }
                }
            }
        }
    }
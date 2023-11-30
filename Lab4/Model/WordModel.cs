using System.ComponentModel.DataAnnotations;

namespace Lab4.Model;

// Модель данных для слова
public class WordModel
{
    [Key] public int Id { get; set; }
    public string? Word { get; set; }
    public string? Construction { get; set; }
    public string? Root { get; set; }
}
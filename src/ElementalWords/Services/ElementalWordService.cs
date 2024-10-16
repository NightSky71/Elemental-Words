using ElementalWords.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ElementalWords.Services;

public class ElementalWordService
{
    private readonly Dictionary<string, ElementModel> _Elements;

    public ElementalWordService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "ElementalWords.Data.Elements.json";

        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        using StreamReader reader = new StreamReader(stream);

        var elements = JsonSerializer.Deserialize<IEnumerable<ElementModel>>(reader.ReadToEnd())
            ?? throw new JsonException("Failed to retrieve elements from Elements.json");

        _Elements = elements.ToDictionary(x => x.Symbol.ToLower(), x => x);
    }

    public List<List<string>> GetElementWords(string word)
    {
        if (string.IsNullOrEmpty(word))
            return new();

        if (!Regex.IsMatch(word, @"^[a-zA-Z]+$"))
            throw new ArgumentException($"{word} can only contain letters");

        var elementalWords = ProcessElementalWords(word.ToLower());

        return elementalWords;
    }

    private List<List<string>> ProcessElementalWords(string str)
    {
        var elementalWords = new List<List<string>>();

        for (int i = 1; i <= 3; i++)
        {
            if(str.Length < i)
                continue;

            if (!_Elements.TryGetValue(str.Substring(0, i), out var value))
                continue;

            if (string.IsNullOrEmpty(str.Substring(i)))
            {
                elementalWords.Add( new() { $"{value.Element} ({value.Symbol})" } );
                continue;
            }

            var results = ProcessElementalWords(str.Substring(i));

            foreach (var elementWord in results)
            {
                elementWord.Insert(0, $"{value.Element} ({value.Symbol})");
            }

            elementalWords.AddRange(results);
        }

        return elementalWords;
    }

}

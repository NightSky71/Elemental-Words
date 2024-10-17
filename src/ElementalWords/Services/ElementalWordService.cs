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
            ?? throw new JsonException("Failed to deserialize ElementModels from from Elements.json");

        _Elements = elements.ToDictionary(x => x.Symbol.ToLower(), x => x);
    }

    public List<List<string>> TransformWordIntoElementWords(string word)
    {
        if (string.IsNullOrEmpty(word))
            return new();

        if (!Regex.IsMatch(word, @"^[a-zA-Z]+$"))
            throw new ArgumentException($"Cannot process inputted word, the word: \"{word}\" can only contain letters");

        return ProcessElementalWords(word.ToLower());
    }

    private List<List<string>> ProcessElementalWords(string str)
    {
        var elementalWords = new List<List<string>>();

        for (int symbolLength = 1; symbolLength <= 2; symbolLength++)
        {
            if(str.Length < symbolLength)
                continue;

            if (!_Elements.TryGetValue(str.Substring(0, symbolLength), out var value))
                continue;

            // If there is a matching element take the remaining string
            var remainingString = str.Substring(symbolLength);

            if (string.IsNullOrEmpty(remainingString))
            {
                elementalWords.Add( new() { $"{value.Element} ({value.Symbol})" } );
                continue;
            }

            // Recursively call the function again to process the remaining string
            var results = ProcessElementalWords(remainingString);

            foreach (var elementWord in results)
            {
                elementWord.Insert(0, $"{value.Element} ({value.Symbol})");
            }

            elementalWords.AddRange(results);
        }

        return elementalWords;
    }

}

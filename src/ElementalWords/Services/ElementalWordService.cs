using ElementalWords.Exceptions;
using ElementalWords.Models;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ElementalWords.Services;

public class ElementalWordService
{
    private readonly Dictionary<string, ElementModel> _elements;

    public ElementalWordService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "ElementalWords.Data.Elements.json";

        using Stream stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new Exception($"Cannot find resource: {resourceName}");

        using StreamReader reader = new StreamReader(stream);

        var elements = JsonSerializer.Deserialize<IEnumerable<ElementModel>>(reader.ReadToEnd())
            ?? throw new JsonException("Failed to deserialize ElementModels from from Elements.json");

        _elements = elements.ToDictionary(x => x.Symbol.ToLower(), x => x);
    }

    public List<List<string>> TransformWordIntoElementalWords(string word)
    {
        if (string.IsNullOrEmpty(word))
            throw new ElementalWordsValidationException($"Cannot process inputted word as it is null or empty");

        if (!Regex.IsMatch(word, @"^[a-zA-Z]+$"))
            throw new ElementalWordsValidationException($"Cannot process inputted word, the word: \"{word}\" can only contain letters");

        return ProcessStringIntoElementalSymbols(word.ToLower());
    }

    private List<List<string>> ProcessStringIntoElementalSymbols(string str)
    {
        var elementalWords = new List<List<string>>();

        for (int symbolLength = 1; symbolLength <= 2; symbolLength++)
        {
            if(str.Length < symbolLength)
                continue;

            if (!_elements.TryGetValue(str.Substring(0, symbolLength), out var value))
                continue;

            // If there is a matching element take the remaining string
            var remainingString = str.Substring(symbolLength);

            if (string.IsNullOrEmpty(remainingString))
            {
                elementalWords.Add( new() { $"{value.Element} ({value.Symbol})" } );
                continue;
            }

            // Recursively call the function again to process the remaining (not empty) string
            var results = ProcessStringIntoElementalSymbols(remainingString);

            foreach (var elementWord in results)
            {
                elementWord.Insert(0, $"{value.Element} ({value.Symbol})");
            }

            elementalWords.AddRange(results);
        }

        return elementalWords;
    }

}

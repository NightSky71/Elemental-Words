using ElementalWords.Models;
using System.Reflection;
using System.Text.Json;

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
        List<List<string>> elements = [];

        if (string.IsNullOrEmpty(word))
            return elements;

        var elementalStrings = ProcessElementalWords(word.ToLower());

        foreach (var elementalString in elementalStrings)
        {
            elements.Add(elementalString.Split(",").ToList());
        }

        return elements;
    }

    private List<string> ProcessElementalWords(string str)
    {
        List<string> elements = new List<string>();

        if (string.IsNullOrEmpty(str))
            return new();

        ElementModel value;

        for (int i = 1; i <= 3; i++)
        {
            if (str.Length >= i && _Elements.TryGetValue(str.Substring(0, i), out value))
            {
                if (string.IsNullOrEmpty(str.Substring(i)))
                {
                    elements.Add($"{value.Element} ({value.Symbol})");
                }
                else
                {
                    var results = ProcessElementalWords(str.Substring(i));

                    foreach (var result in results)
                    {
                        elements.Add($"{value.Element} ({value.Symbol}),{result}");
                    }
                }
            }
        }

        return elements;
    }

}

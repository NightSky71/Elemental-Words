using ElementalWords.Exceptions;
using ElementalWords.Services;

Console.WriteLine("Elemental Words");

var elementalWordService = new ElementalWordService();

while (true)
{
    try
    {
        Console.WriteLine();
        Console.Write("Input Word: ");
        var input = Console.ReadLine();

        Console.WriteLine();

        var elementalWords = elementalWordService.ValidateAndProcessWord(input);

        if (elementalWords.Count == 0)
        {
            Console.WriteLine($"No sequence of elemental symbols can be found for the word: {input}");
            continue;
        }
            
        foreach (var elementalWord in elementalWords)
        {
            Console.WriteLine(string.Join(',', elementalWord));
        }
    }
    catch (ElementalWordsValidationException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

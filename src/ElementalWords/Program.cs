// See https://aka.ms/new-console-template for more information
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

        var elementalWords = elementalWordService.TransformWordIntoElementWords(input);

        foreach (var elementalWord in elementalWords)
        {
            Console.WriteLine(string.Join(',', elementalWord));
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

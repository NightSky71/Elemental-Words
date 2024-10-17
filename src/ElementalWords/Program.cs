// See https://aka.ms/new-console-template for more information
using ElementalWords.Services;

Console.WriteLine("Elemental Words");

var elementalWordService = new ElementalWordService();

while (true)
{
    Console.Write("Input Word: ");
    var input = Console.ReadLine();

    var elementalWords = elementalWordService.TransformWordIntoElementWords(input);

    foreach (var elementalWord in elementalWords)
    {
        Console.WriteLine(string.Join(',', elementalWord));
    }
}

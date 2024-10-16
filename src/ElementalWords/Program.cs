// See https://aka.ms/new-console-template for more information
using ElementalWords.Services;

Console.WriteLine("Elemental Words");

var elementalWordService = new ElementalWordService();

while (true)
{
    Console.Write("Input Word: ");
    var input = Console.ReadLine();

    var result = elementalWordService.GetElementWords(input);

    foreach (var elementalWord in result)
    {
        Console.WriteLine(string.Join(',', elementalWord));
    }
}

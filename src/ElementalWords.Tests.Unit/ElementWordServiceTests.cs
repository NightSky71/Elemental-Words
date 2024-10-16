using ElementalWords.Models;
using ElementalWords.Services;
using FluentAssertions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElementalWords.Tests.Unit
{
    public class ElementWordServiceTests
    {
        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] 
            { 
                "Yes",
                new List<List<string>>(){ 
                    new() 
                    { 
                        "Yttrium (Y)", "Einsteinium (Es)"
                    } 
                }
            },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Given_AWord_When_ElementWordServiceIsCalled_Then_OutputTheExpectSequencesOfElementalSymbols(string word, List<List<string>> expectedResult)
        {
            // Arrange
            var elementalWordService = new ElementalWordService();

            // Act
            var results = elementalWordService.GetElementWords(word);

            // Assert
            results.Should().BeEquivalentTo(expectedResult);
        }
    }
}
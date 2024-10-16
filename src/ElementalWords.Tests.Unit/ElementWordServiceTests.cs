using ElementalWords.Services;
using FluentAssertions;

namespace ElementalWords.Tests.Unit
{
    public class ElementWordServiceTests
    {
        public static IEnumerable<object[]> ValidWordsMemberData =>
        new List<object[]>
        {
            new object[] 
            { 
                "Yes",
                new List<List<string>>(){ 
                    new() { "Yttrium (Y)", "Einsteinium (Es)" } 
                }
            },
            new object[]
            {
                "Snack",
                new List<List<string>>(){
                    new() { "Sulfur (S)", "Nitrogen (N)", "Actinium (Ac)", "Potassium (K)" },
                    new() { "Sulfur (S)", "Sodium (Na)", "Carbon (C)", "Potassium (K)" },
                    new() { "Tin (Sn)", "Actinium (Ac)", "Potassium (K)" }
                }
            },
            new object[]
            {
                "Beach",
                new List<List<string>>(){
                    new() { "Beryllium (Be)", "Actinium (Ac)", "Hydrogen (H)" },
                }
            },
            new object[]
            {
                "Nag",
                new List<List<string>>(){
                    new() { "Nitrogen (N)", "Silver (Ag)" },
                }
            },
            new object[]
            {
                "Coco",
                new List<List<string>>(){
                    new() { "Carbon (C)", "Oxygen (O)", "Carbon (C)", "Oxygen (O)" },
                    new() { "Carbon (C)", "Oxygen (O)", "Cobalt (Co)" },
                    new() { "Cobalt (Co)", "Carbon (C)", "Oxygen (O)" },
                    new() { "Cobalt (Co)", "Cobalt (Co)" },
                }
            },
        };

        [Theory]
        [MemberData(nameof(ValidWordsMemberData))]
        public void Given_AValidWord_When_ElementWordServiceIsCalled_Then_OutputTheExpectSequencesOfElementalSymbols(string word, List<List<string>> expectedResult)
        {
            // Arrange
            var elementalWordService = new ElementalWordService();

            // Act
            var results = elementalWordService.GetElementWords(word);

            // Assert
            results.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("snack")]
        [InlineData("sNaCK")]
        [InlineData("SNACK")]
        public void Given_AValidWordWithDifferingCaptilisation_When_ElementWordServiceIsCalled_Then_OutputTheExpectSequencesOfElementalSymbols(string word)
        {
            // Arrange
            var elementalWordService = new ElementalWordService();

            var expectedResult = new List<List<string>>(){
                    new() { "Sulfur (S)", "Nitrogen (N)", "Actinium (Ac)", "Potassium (K)" },
                    new() { "Sulfur (S)", "Sodium (Na)", "Carbon (C)", "Potassium (K)" },
                    new() { "Tin (Sn)", "Actinium (Ac)", "Potassium (K)" }
                };

            // Act
            var results = elementalWordService.GetElementWords(word);

            // Assert
            results.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("Yes1")]
        [InlineData("2123")]
        [InlineData("*(!£$")]
        public void Given_AnInvalidWordThatContainsSomeNonLetters_When_ElementWordServiceIsCalled_Then_ThrowArugmentException(string word)
        {
            // Arrange
            var elementalWordService = new ElementalWordService();

            // Act
            Action act = () => elementalWordService.GetElementWords(word);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"{word} can only contain letters");
        }
    }
}
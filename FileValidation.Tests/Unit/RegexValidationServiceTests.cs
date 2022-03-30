using FileValidation.Services.Implementation;
using FileValidation.Tests.Base;
using FluentAssertions;
using Xunit;

namespace FileValidation.Tests.Unit
{
    public class RegexValidationServiceTests : TestBase<RegexValidationService>
    {
        public RegexValidationServiceTests()
        {
            Target = new RegexValidationService();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("albert")]
        [InlineData("ALbert")]
        [InlineData("Albe1t")]
        public void Executed_IsAccountNameValid_With_Invalid_Arg_Should_Return_False(string accountName)
        {
            var result = Target.IsAccountNameValid(accountName);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Albert")]
        [InlineData("Nick")]
        [InlineData("Michael")]
        public void Executed_IsAccountNameValid_With_Valid_Arg_Should_Return_True(string accountName)
        {
            var result = Target.IsAccountNameValid(accountName);
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("1299991")]
        [InlineData("2299921")]
        [InlineData("5299991")]
        [InlineData("6299921")]
        [InlineData("7299921")]
        [InlineData("8299991")]
        [InlineData("9299921")]
        [InlineData("0299921")]
        [InlineData("1299991p")]
        [InlineData("2299921p")]
        [InlineData("5299991p")]
        [InlineData("6299921p")]
        [InlineData("7299921p")]
        [InlineData("8299991p")]
        [InlineData("9299921p")]
        [InlineData("0299921p")]
        [InlineData("32999213")]
        [InlineData("42999213")]
        [InlineData("32999913p")]
        [InlineData("42999213p")]
        public void Executed_IsAccountNumbersValid_With_Invalid_Arg_Should_Return_False(string accountNumbers)
        {
            var result = Target.IsAccountNumbersValid(accountNumbers);
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("3299991")]
        [InlineData("4299921")]
        [InlineData("4299921p")]
        [InlineData("3299921p")]
        public void Executed_IsAccountNumbersValid_With_Valid_Arg_Should_Return_True(string accountNumbers)
        {
            var result = Target.IsAccountNumbersValid(accountNumbers);
            result.Should().BeTrue();
        }
    }
}

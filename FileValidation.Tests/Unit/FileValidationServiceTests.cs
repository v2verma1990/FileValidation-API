using FileValidation.Models;
using FileValidation.Services.Implementation;
using FileValidation.Tests.Base;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileValidation.Tests.Unit
{
    public class FileValidationServiceTests : TestBase<FileValidationService>
    {
        private Mock<ILogger<FileValidationService>> mockLogger = new();

        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        public FileValidationServiceTests()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            Target = new FileValidationService(new RegexValidationService(), mockLogger.Object);
        }

        [Fact]
        public async void ExecutedValidateFileAsync_With_Data_Set()
        {
            var testFileContent =
                File.ReadAllText(Path.Combine("Assets", "FileValidationServiceTestData.txt"));
            var expectedInvalidLines =
                File.ReadAllLines(Path.Combine("Assets", "FileValidationServiceTestDataExpectedOutput.txt"));

            var actualValidationResult = await Target.ValidateFileAsync(new FileValidationModel
            {
                FileContent = testFileContent
            }, cancellationToken);

            var expectedValidationResut = new FileValidationResultModel
            {
                FileValid = !expectedInvalidLines.Any(),
                InvalidLines = expectedInvalidLines
            };

            actualValidationResult.Should().NotBeNull();
            actualValidationResult.Should().BeEquivalentTo(expectedValidationResut);
        }

        [Fact]
        public async Task CancellationCalled_ShouldThrow_OperationCanceledException()
        {
            cancellationTokenSource.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                await Target.ValidateFileAsync(new FileValidationModel
                {
                    FileContent = ""
                }, cancellationToken);
            });
        }
    }
}

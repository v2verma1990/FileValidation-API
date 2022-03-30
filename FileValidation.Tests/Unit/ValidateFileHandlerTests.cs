using AutoMapper;
using FileValidation.Mediatr.Handlers;
using FileValidation.Mediatr.Requests;
using FileValidation.Mediatr.Responses;
using FileValidation.Models;
using FileValidation.Services.Abstraction;
using FileValidation.Tests.Base;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FileValidation.Tests.Unit
{
    public class ValidateFileHandlerTests : TestBase<ValidateFileHandler>
    {
        private Mock<IFileValidationService> mockFileValidationService = new();
        private Mock<IMapper> mockMapper = new();

        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        private ValidateFileRequest actualValidateFileRequest;
        private ValidateFileResponse expectedValidateFileResponse;
        private FileValidationModel actualFileValidationModel;
        private FileValidationResultModel actualFileValidationResultModel;

        public ValidateFileHandlerTests()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            actualValidateFileRequest = new ValidateFileRequest();
            expectedValidateFileResponse = new ValidateFileResponse();
            actualFileValidationModel = new FileValidationModel();
            actualFileValidationResultModel = new FileValidationResultModel();

            mockMapper.Setup((x) => x.Map<FileValidationModel>(actualValidateFileRequest))
                .Returns(actualFileValidationModel);
            mockMapper.Setup((x) => x.Map<ValidateFileResponse>(actualFileValidationResultModel))
                .Returns(expectedValidateFileResponse);
            mockFileValidationService.Setup((x) => x.ValidateFileAsync(actualFileValidationModel, cancellationToken))
                .ReturnsAsync(actualFileValidationResultModel);

            Target = new ValidateFileHandler(
                    mockFileValidationService.Object,
                    mockMapper.Object);
        }

        [Fact]
        public void ProvidedNullArgument_As_FileValidationService_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var obj = new ValidateFileHandler(
                    null,
                    mockMapper.Object);
            });
        }

        [Fact]
        public void ProvidedNullArgument_As_Mapper_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var obj = new ValidateFileHandler(
                    mockFileValidationService.Object,
                    null);
            });
        }

        [Fact]
        public async Task CancellationCalled_ShouldThrow_OperationCanceledException()
        {
            cancellationTokenSource.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                await Target.Handle(
                    actualValidateFileRequest,
                    cancellationToken);
            });
        }

        [Fact]
        public async Task ExecutedWithValidParams_Should_Return_ValidateFileResponse()
        {
            var actualResult = await Target.Handle(
                    actualValidateFileRequest,
                    cancellationToken);

            actualResult.Should().NotBeNull();
            actualResult.Should().Be(expectedValidateFileResponse);
        }
    }
}

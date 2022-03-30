using AutoMapper;
using FileValidation.Mediatr.Requests;
using FileValidation.Mediatr.Responses;
using FileValidation.Models;
using FileValidation.Services.Abstraction;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FileValidation.Mediatr.Handlers
{
    public class ValidateFileHandler : IRequestHandler<ValidateFileRequest, ValidateFileResponse>
    {
        private readonly IFileValidationService _fileValidationService;
        private readonly IMapper _mapper;

        public ValidateFileHandler(
            IFileValidationService fileValidationService,
            IMapper mapper)
        {
            _fileValidationService = fileValidationService ?? throw new ArgumentNullException(nameof(fileValidationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ValidateFileResponse> Handle(ValidateFileRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var fileValidationModel = _mapper.Map<FileValidationModel>(request);

            var validationResultModel = await _fileValidationService.ValidateFileAsync(fileValidationModel, cancellationToken);

            var validationResultResponse = _mapper.Map<ValidateFileResponse>(validationResultModel);

            return validationResultResponse;
        }
    }
}

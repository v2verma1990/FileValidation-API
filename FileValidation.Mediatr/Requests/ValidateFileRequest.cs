using FileValidation.Mediatr.Responses;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Mediatr.Requests
{
    [ExcludeFromCodeCoverage]
    public class ValidateFileRequest : IRequest<ValidateFileResponse>
    {
        public string FileContent { get; set; }
    }
}

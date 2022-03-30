using AutoMapper;
using FileValidation.Mediatr.Requests;
using FileValidation.Mediatr.Responses;
using FileValidation.Models;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Mediatr.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class MediatrServiceModelsProfile : Profile
    {
        public MediatrServiceModelsProfile()
        {
            CreateMap<ValidateFileRequest, FileValidationModel>();
            CreateMap<FileValidationResultModel, ValidateFileResponse>();
        }
    }
}

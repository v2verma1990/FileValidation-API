using AutoMapper;
using FileValidation.Dtos;
using FileValidation.Mediatr.Responses;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Web.AutoMapper
{
    [ExcludeFromCodeCoverage]
    public class DtoMediatrModelsProfile : Profile
    {
        public DtoMediatrModelsProfile()
        {
            CreateMap<ValidateFileResponse, FileValidationResultDto>();
        }
    }
}

using FileValidation.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FileValidation.Services.Abstraction
{
    public interface IFileValidationService
    {
        Task<FileValidationResultModel> ValidateFileAsync(FileValidationModel fileValidationModel, CancellationToken cancellationToken);
    }
}

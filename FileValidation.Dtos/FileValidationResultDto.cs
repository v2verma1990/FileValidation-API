using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Dtos
{
    [ExcludeFromCodeCoverage]
    public class FileValidationResultDto
    {
        public bool FileValid { get; set; }

        public IEnumerable<string> InvalidLines { get; set; }
    }
}

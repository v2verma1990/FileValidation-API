using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Models
{
    [ExcludeFromCodeCoverage]
    public class FileValidationResultModel
    {
        public bool FileValid { get; set; }

        public IEnumerable<string> InvalidLines { get; set; }
    }
}

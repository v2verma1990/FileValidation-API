using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FileValidation.Mediatr.Responses
{
    [ExcludeFromCodeCoverage]
    public class ValidateFileResponse
    {
        public bool FileValid { get; set; }

        public IEnumerable<string> InvalidLines { get; set; }
    }
}

using FileValidation.Services.Abstraction;
using System.Text.RegularExpressions;

namespace FileValidation.Services.Implementation
{
    public class RegexValidationService : IRegexValidationService
    {
        public bool IsAccountNameValid(string accountName)
        {
            if (accountName is null)
            {
                return false;
            }

            return Regex.IsMatch(accountName, @"^([A-Z])[a-z]+$");
        }

        public bool IsAccountNumbersValid(string accountNumbers)
        {
            if (accountNumbers is null)
            {
                return false;
            }

            return Regex.IsMatch(accountNumbers, @"(^[34][0-9]{6})($|p$)");
        }
    }
}

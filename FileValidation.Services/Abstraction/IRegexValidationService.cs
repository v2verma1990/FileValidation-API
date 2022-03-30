namespace FileValidation.Services.Abstraction
{
    public interface IRegexValidationService
    {
        bool IsAccountNameValid(string accountName);

        bool IsAccountNumbersValid(string accountNumbers);
    }
}

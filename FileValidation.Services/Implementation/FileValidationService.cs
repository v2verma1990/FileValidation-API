using FileValidation.Models;
using FileValidation.Services.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileValidation.Services.Implementation
{
    public class FileValidationService : IFileValidationService
    {
        private readonly IRegexValidationService _regexValidationService;
        private readonly ILogger<FileValidationService> _logger;

        public FileValidationService(
            IRegexValidationService regexValidationService,
            ILogger<FileValidationService> logger)
        {
            _regexValidationService = regexValidationService ?? throw new ArgumentNullException(nameof(regexValidationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<FileValidationResultModel> ValidateFileAsync(FileValidationModel fileValidationModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var fileLines = fileValidationModel.FileContent.Split("\n");

            var invalidLines = new List<string>();

            var stopWatch = new Stopwatch();

            for (int i = 0; i < fileLines.Length; i++)
            {
                stopWatch.Restart();

                var line = RemoveAllSpecialCharactersFromString(fileLines[i]);

                var detailsSplitted = line.Split(" ");
                var accountName = detailsSplitted.FirstOrDefault();
                var accountNumbers = detailsSplitted.Skip(1).FirstOrDefault();

                var isAccountNameValid = _regexValidationService.IsAccountNameValid(accountName);
                var isAccountNumbersValid = _regexValidationService.IsAccountNumbersValid(accountNumbers);

                if (detailsSplitted.Length != 2 ||
                    (!isAccountNameValid && !isAccountNumbersValid))
                {
                    invalidLines.Add($"Account name, account number - not valid for {i + 1} line '{line}'");
                }
                else if (!isAccountNameValid)
                {
                    invalidLines.Add($"Account name - not valid for {i + 1} line '{line}'");
                }
                else if (!isAccountNumbersValid)
                {
                    invalidLines.Add($"Account number - not valid for {i + 1} line '{line}'");
                }

                stopWatch.Stop();

                _logger.LogInformation("Time spent on validating line '{0}' = {1}", line, stopWatch.Elapsed);
            }

            return Task.FromResult(new FileValidationResultModel
            {
                FileValid = !invalidLines.Any(),
                InvalidLines = invalidLines
            });
        }

        private string RemoveAllSpecialCharactersFromString(string str)
        {
            return Regex.Replace(str, @"[^\u0020-\u007F]", string.Empty);
        }
    }
}

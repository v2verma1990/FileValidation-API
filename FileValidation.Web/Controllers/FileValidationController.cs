using AutoMapper;
using FileValidation.Dtos;
using FileValidation.Mediatr.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileValidation.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileValidationController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public FileValidationController(
            IMediator mediatr,
            IMapper mapper)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> ValidateFileAsync(IFormFile file, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (file is null)
            {
                return BadRequest("File is required");
            }

            var fileContent = string.Empty;

            using (var stream = file.OpenReadStream())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    fileContent = await streamReader.ReadToEndAsync();
                }
            }

            var validateFileRequest = new ValidateFileRequest
            {
                FileContent = fileContent
            };

            var validationResult = await _mediatr.Send(validateFileRequest, cancellationToken);

            var validationResultDto = _mapper.Map<FileValidationResultDto>(validationResult);

            return Ok(validationResultDto.FileValid ? new { FileValid = true } : validationResultDto);
        }
    }
}

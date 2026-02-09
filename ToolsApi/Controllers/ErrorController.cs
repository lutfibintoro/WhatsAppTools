using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToolsApi.Models.Dto.Response;

namespace ToolsApi.Controllers
{
    public partial class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/exception")]
        public IActionResult Exception()
        {
            IExceptionHandlerFeature? exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerFeature is null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            //if (Request.Headers.Accept.ToString().Contains("text/html"))
            //    return PageException(exceptionHandlerFeature.Error);
            //else
            return ApiException(exceptionHandlerFeature.Error);
        }
    }





    public partial class ErrorController
    {
        private IActionResult PageException(Exception ex)
        {
            if (ex is KeyNotFoundException)
                return NotFound();

            if (ex is NotImplementedException)
                return BadRequest();

            if (ex is ArgumentException)
                return BadRequest();

            if (ex is InvalidOperationException)
                return StatusCode(StatusCodes.Status503ServiceUnavailable);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        private IActionResult ApiException(Exception ex)
        {
            ErrorResponseDto errorResponseDto = new()
            {
                Message = ex.Message
            };

            if (ex is WebSocketException)
                return StatusCode(StatusCodes.Status426UpgradeRequired, errorResponseDto);

            if (ex is KeyNotFoundException)
                return NotFound(errorResponseDto);

            if (ex is InvalidOperationException)
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponseDto);

            if (ex is ArgumentException)
                return BadRequest(errorResponseDto);

            if (ex is BadHttpRequestException)
                return BadRequest(errorResponseDto);

            return StatusCode(StatusCodes.Status500InternalServerError, errorResponseDto);
        }
    }
}

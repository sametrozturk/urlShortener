using Application.Common;
using Application.Exceptions;
using Application.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Route("api/Error")]
[ApiController]
public class ErrorController : Controller
{
    private readonly IApplicationDbContext _context;

    public ErrorController(IApplicationDbContext context)
    {
        _context = context;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<JsonResult> ErrorAsync()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
        string message = string.Empty;
        int result = 0;

        switch (exception)
        {
            case UrlShortenerExceptions:
                result = ((UrlShortenerExceptions)exception).ReasonCode;
                try
                {
                    var responseMessage = await this._context.ResponseMessages
                        .Where(r => r.ReasonCode == result)
                        .FirstOrDefaultAsync(new CancellationToken());

                    if (responseMessage is null)
                    {
                        this.HttpContext.Response.StatusCode = 422;
                        message = $"Undefined Error Code: {result}";

                        break;
                    }

                    message = responseMessage.Message;

                }
                catch (Exception ex)
                {

                    message = ex.Message + result;
                }
                break;
            default:
                result = CustomErrorCodes.UNHANDLED_EXCEPTION;
                message = exception?.InnerException?.Message + exception?.StackTrace;
                break;
        }
        return Json(new UrlShortenerResult(result, message));
    }
}
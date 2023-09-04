using Api.Models;
using Application.ShortenUrl.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers;


[Route("api/")]
[ApiController]
public class UrlShortenerController : ControllerBase
{

    private readonly IMediator _mediator;

    public UrlShortenerController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost("shortenUrl")]
    public async Task<ActionResult> ShortenUrl([FromBody] UrlRequestModel model)
    {
        var command = new ShortenUrlCommand(model.Url);
        var result = await this._mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("shortenUrlWithCustomHash")]
    public async Task<ActionResult> ShortenUrlWithCustomHash([FromBody] CustomUrlRequestModel model)
    {
        var command = new ShortenUrlWithCustomHashCommand(model.Url, model.CustomHash);
        var result = await this._mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("{hash}")]
    public async Task<ActionResult> RedirectShortenedUrl([Required] string hash)
    {
        var command = new RedirectShortenedUrlCommand(hash);
        var result = await this._mediator.Send(command);

        return Redirect(result.Message);
    }

}

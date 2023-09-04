namespace Application.Common;

public class UrlShortenerResult
{
    public int? Code { get; private set; }
    public string Message { get; private set; }
    public string Description { get; private set; } = string.Empty;

    public UrlShortenerResult(int code, string msg)
    {
        this.Code = code;
        this.Message = msg;
    }

    public UrlShortenerResult(string msg)
    {
        this.Message = msg;
    }

    public UrlShortenerResult(int code, string msg, string description)
    {
        this.Code = code;
        this.Message = msg;
        this.Description = description;
    }
}

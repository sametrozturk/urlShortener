namespace Application.Exceptions;

public class UrlShortenerExceptions : Exception
{
    public int ReasonCode { get; set; }
    public UrlShortenerExceptions(int reasonCode)
    {
        ReasonCode = reasonCode;
    }
}

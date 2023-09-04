using Application.Common;

namespace Application.ShortenUrl.Services;

public class UrlValidatorService : IUrlValidatorService
{

    public UrlValidatorService()
    {
    }

    public bool IsUrlValid(string url)
    {
        Uri uriResult;

        if (Uri.TryCreate(url, UriKind.Absolute, out uriResult))
        {
            if (uriResult.Host != null && uriResult.Host.Length > 0)
            {
                return true;
            }
        }

        return false;
    }

}

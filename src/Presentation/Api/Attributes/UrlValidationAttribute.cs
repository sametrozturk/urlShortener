using System.ComponentModel.DataAnnotations;

public class UrlValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }
        }

        return false;
    }
}
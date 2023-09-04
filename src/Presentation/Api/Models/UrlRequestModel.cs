using Application.Common;

namespace Api.Models;

public struct UrlRequestModel : IUrlRequestModel
{

    [UrlValidationAttribute]
    public string Url { get; set; }

    public UrlRequestModel(string url)
    {
        this.Url = url;
    }

}
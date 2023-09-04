using Application.Common;

namespace Api.Models;

public struct CustomUrlRequestModel : IUrlRequestModel
{
    [UrlValidationAttribute]
    public string Url { get; set; }
    [CustomUrlLengthAttribute]
    public string CustomHash { get; set; }

    public CustomUrlRequestModel(string url, string customHash)
    {
        this.Url = url;
        this.CustomHash = customHash;
    }

}
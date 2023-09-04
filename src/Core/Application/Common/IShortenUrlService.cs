using Application.ShortenUrl.Models;
using Domain.ShortenedUrl;

namespace Application.Common;

public interface IShortenUrlService
{
    public Task<Tuple<bool, ShortenedUrl?>> IsCustomUrlExist(string url, string customUrl);
    public Task<string> GenerateHash();
    public SeperateHostAndRouteDto SeperateHostAndRoute(string url);
}

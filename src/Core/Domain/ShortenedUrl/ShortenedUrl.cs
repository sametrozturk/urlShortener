namespace Domain.ShortenedUrl;

public class ShortenedUrl
{

    public int Id { get; private set; }
    public string ApiHostUrl { get; private set; }
    public string GivenUrl { get; private set; }
    public string GivenUrlSchema { get; private set; }
    public string GivenUrlHost { get; private set; }
    public string GivenUrlRouteAndQuery { get; private set; }
    public string ShortUrl { get; private set; }
    public bool IsCustomHash { get; private set; }
    public string Hash { get; private set; }


    public static ShortenedUrl CreateNew(
        string apiHostUrl,
        string givenUrl,
        string givenUrlSchema,
        string givenUrlHost,
        string givenUrlRouteAndQuery,
        string shortUrl,
        bool isCustomHash,
        string hash)
    {
        return new ShortenedUrl(
            apiHostUrl,
            givenUrl,
            givenUrlSchema,
            givenUrlHost,
            givenUrlRouteAndQuery,
            shortUrl,
            isCustomHash,
            hash);
    }

    private ShortenedUrl(
        string apiHostUrl,
        string givenUrl,
        string givenUrlSchema,
        string givenUrlHost,
        string givenUrlRouteAndQuery,
        string shortUrl,
        bool isCustomHash,
        string hash)
    {
        this.ApiHostUrl = apiHostUrl;
        this.GivenUrl = givenUrl;
        this.GivenUrlSchema = givenUrlSchema;
        this.GivenUrlHost = givenUrlHost;
        this.GivenUrlRouteAndQuery = givenUrlRouteAndQuery;
        this.ShortUrl = shortUrl;
        this.IsCustomHash = isCustomHash;
        this.Hash = hash;
    }

}
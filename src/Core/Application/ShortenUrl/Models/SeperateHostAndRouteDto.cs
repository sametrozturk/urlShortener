namespace Application.ShortenUrl.Models
{
    public record SeperateHostAndRouteDto
    {
        public string Host { get; set; }
        public string Scheme { get; set; }
        public string Route { get; set; }

    }
}

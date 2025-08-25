namespace LastMinuteLite.Web.Models;

public class ServiceEndpointsOptions
{
    public ApiService Airplane { get; set; } = new();
    public ApiService Hotel { get; set; } = new();
    public ApiService Payment { get; set; } = new(); // dritter Service (Platzhalter)

    public class ApiService
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string ApiToken { get; set; } = string.Empty;
    }
}

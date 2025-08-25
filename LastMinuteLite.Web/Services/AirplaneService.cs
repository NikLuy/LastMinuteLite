using LastMinuteLite.Shared;

namespace LastMinuteLite.Web.Services;

public interface IAirplaneService
{
    Task<FlightDealDto?> GetDealAsync(CancellationToken ct = default);
    Task<FlightDealDto?> GetComboDealAsync(CancellationToken ct = default);
}

public class AirplaneService(HttpClient http) : IAirplaneService
{
    public async Task<FlightDealDto?> GetDealAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<FlightDealDto>("/api/deals/random", ct);

    public async Task<FlightDealDto?> GetComboDealAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<FlightDealDto>("/api/deals/combo", ct);
}

using LastMinuteLite.Shared;

namespace LastMinuteLite.Web.Services;

public interface IHotelService
{
    Task<HotelDealDto?> GetDealAsync(CancellationToken ct = default);
}

public class HotelService(HttpClient http) : IHotelService
{
    public async Task<HotelDealDto?> GetDealAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<HotelDealDto>("/api/deals/random", ct);
}
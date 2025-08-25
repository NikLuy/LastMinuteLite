using LastMinuteLite.Shared;
using static System.Net.WebRequestMethods;

namespace LastMinuteLite.Web.Services;

public interface IHotelService
{
    Task<HotelDealDto?> GetDealAsync(DateTime? date = null, CancellationToken ct = default);
}

public class HotelService(HttpClient http) : IHotelService
{
    public async Task<HotelDealDto?> GetDealAsync(DateTime? date = null, CancellationToken ct = default)
    {
        if (date.HasValue)
        {
            var dateString = date.Value.ToString("yyyy-MM-dd");
            return await http.GetFromJsonAsync<HotelDealDto>($"/api/deals/random?{dateString}", ct);
        }
        return await http.GetFromJsonAsync<HotelDealDto>("/api/deals/random", ct);
    }

}
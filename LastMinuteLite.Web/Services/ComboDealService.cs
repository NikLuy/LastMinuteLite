using System.Net.Http.Json;
using LastMinuteLite.Shared;

namespace LastMinuteLite.Web.Services;

public interface IComboDealService
{
    Task SaveAsync(ComboDealDto deal, CancellationToken ct = default);
    Task<IReadOnlyList<ComboDealDto>> GetRecentAsync(CancellationToken ct = default);
}

public class ComboDealService(HttpClient http) : IComboDealService
{
    private readonly HttpClient _http = http;

    public async Task SaveAsync(ComboDealDto deal, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync("/api/combos", deal, ct);
        resp.EnsureSuccessStatusCode();
    }

    public async Task<IReadOnlyList<ComboDealDto>> GetRecentAsync(CancellationToken ct = default)
    {
        var data = await _http.GetFromJsonAsync<List<ComboDealDto>>("/api/combos", ct) ?? [];
        return data;
    }
}

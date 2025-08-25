using LastMinuteLite.Shared;

namespace LastMinuteLite.Web.Entities;

public class ComboDeal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

    // Flight snapshot
    public Guid FlightId { get; set; }
    public string Airline { get; set; } = string.Empty;
    public string FlightFrom { get; set; } = string.Empty;
    public string FlightTo { get; set; } = string.Empty;
    public DateTime DepartureUtc { get; set; }
    public decimal FlightPrice { get; set; }
    public int SeatsLeft { get; set; }

    // Hotel snapshot
    public Guid HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int Stars { get; set; }
    public DateTime CheckInUtc { get; set; }
    public int Nights { get; set; }
    public decimal HotelPricePerNight { get; set; }
    public int RoomsLeft { get; set; }

    public decimal Total { get; set; }

    public ComboDealDto ToDto() => new(
        new FlightDealDto(FlightId, Airline, FlightFrom, FlightTo, DepartureUtc, FlightPrice, SeatsLeft),
        new HotelDealDto(HotelId, HotelName, City, Stars, CheckInUtc, Nights, HotelPricePerNight, RoomsLeft),
        Total);

    public static ComboDeal FromDto(ComboDealDto dto) => new()
    {
        FlightId = dto.Flight.Id,
        Airline = dto.Flight.Airline,
        FlightFrom = dto.Flight.From,
        FlightTo = dto.Flight.To,
        DepartureUtc = dto.Flight.DepartureUtc,
        FlightPrice = dto.Flight.Price,
        SeatsLeft = dto.Flight.SeatsLeft,
        HotelId = dto.Hotel.Id,
        HotelName = dto.Hotel.HotelName,
        City = dto.Hotel.City,
        Stars = dto.Hotel.Stars,
        CheckInUtc = dto.Hotel.CheckInUtc,
        Nights = dto.Hotel.Nights,
        HotelPricePerNight = dto.Hotel.PricePerNight,
        RoomsLeft = dto.Hotel.RoomsLeft,
        Total = dto.Total
    };
}

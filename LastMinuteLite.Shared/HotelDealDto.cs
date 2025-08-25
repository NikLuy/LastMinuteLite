namespace LastMinuteLite.Shared;

public record HotelDealDto(
   Guid Id,
   string HotelName,
   string City,
   int Stars,
   DateTime CheckInUtc,
   int Nights,
   decimal PricePerNight,
   int RoomsLeft
);

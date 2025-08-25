using System;

namespace LastMinuteLite.Shared;

public record FlightDealDto(
    Guid Id,
    string Airline,
    string From,
    string To,
    DateTime DepartureUtc,
    decimal Price,
    int SeatsLeft
);

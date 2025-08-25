using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastMinuteLite.Shared;

public record ComboDealDto(
    FlightDealDto Flight, 
    HotelDealDto Hotel, 
    decimal Total
    );

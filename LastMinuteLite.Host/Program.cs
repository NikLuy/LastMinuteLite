var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LastMinuteLite_HotelApi>("lastminutelite-hotelapi");

builder.AddProject<Projects.LastMinuteLite_AirPlaneApi>("lastminutelite-airplaneapi");

builder.AddProject<Projects.LastMinuteLite_Web>("lastminutelite-web");

builder.Build().Run();

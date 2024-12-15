# CodingChallenge - Hotel Manager

# Setup

The .NET 8 Software Development Kit (SDK) is required for building and running this application.
You can download the .NET 8 SDK from the official .NET Download page.


Steps to Build and Run:

1. Navigate to the HotelManagerChallenge directory.
2. Use the following commands to build and run the program:

          dotnet build
          dotnet run --hotels hotels.json --bookings bookings.json


# Using the app
---------------------------------------------------------------------------


The application allows the user to interact with the Hotel Manager through 2 commands


1. Availability(>HotelId<, >RangeOfDates<, >RoomType<) 

- This command displays the number of available rooms of a specific type for a given hotel and date range.
- Note: The result can be negative if the rooms of that type are overbooked.


2. RoomTypes(>HotelId<, >RangeOfDates<, >NumberOfPeople<)


- This command allocates the specified number of people within a hotel over a date range.
- The allocation prioritizes:

     - Minimizing the number of rooms used.
     - Avoiding partially filled rooms.


3. Exiting the Application

- To exit the Hotel Manager application, leave the input blank and press Enter.


---------------------------------------------------------------------------

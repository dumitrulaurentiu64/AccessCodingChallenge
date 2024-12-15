# CodingChallenge - Hotel Manager

# Setup

The .NET 8 Software Development Kit (SDK) is required for building and running this application.
You can download the .NET 8 SDK from the official .NET Download page.


In order to run and test the application the user needs to be inside HotelManagerChallenge directory.
There the user can build and run the application through the following commands

dotnet build
dotnet run --hotels hotels.json --bookings bookings.json


# Using the app
---------------------------------------------------------------------------


The application allows the user to interact with the Hotel Manager through 2 commands


1. Availability(>HotelId<, >RangeOfDates<, >RoomType<) 

    -> This is used to show the number of available rooms of a particular **type** from a **hotel** in a given **time interval** or day.
The command CAN return a negative value due to the fact that the rooms of that type can be overbooked.


2. RoomTypes(>HotelId<, >RangeOfDates<, >NumberOfPeople<)


    -> This is used to allocate given **number of people** in a **time interval** or day in a particular **hotel**.
This should priotize a low number of rooms occupied and the avoidance of partially filled rooms.

Leaving the input blank results in quiting the Hotel Manager application.


---------------------------------------------------------------------------

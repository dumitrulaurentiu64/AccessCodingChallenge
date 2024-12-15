using HotelManagerChallenge.Commands;
using HotelManagerChallenge.Models;
using HotelManagerChallenge.Services;

public class HotelManager
{
    private readonly IFileReader _fileReader;
    private readonly ICommandHandlerRegistry _commandHandlerRegistry;

    public HotelManager(IFileReader fileReader, ICommandHandlerRegistry commandHandlerRegistry)
    {
        _fileReader = fileReader;
        _commandHandlerRegistry = commandHandlerRegistry;
    }

    public void StartManager(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Not arguments provided.");
            return;
        }

        var (hotelsFilePath, bookingsFilePath) = ParseArguments(args);
        if (hotelsFilePath == null || bookingsFilePath == null)
        {
            Console.WriteLine("Both hotels and bookings need to be provided.");
            return;
        }

        var hotels = _fileReader.ReadHotels(hotelsFilePath);
        var bookings = _fileReader.ReadBookings(bookingsFilePath);

        Console.WriteLine("-----------");
        Console.WriteLine("Hotel Manager Online");
        Console.WriteLine("Listening...");
        Console.WriteLine("-----------");

        ProcessUserCommands(hotels, bookings);
    }

    protected (string, string) ParseArguments(string[] args)
    {
        string hotelsFilePath = null;
        string bookingsFilePath = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--hotels" && i + 1 < args.Length)
                hotelsFilePath = args[i + 1];
            else if (args[i] == "--bookings" && i + 1 < args.Length)
                bookingsFilePath = args[i + 1];
        }

        return (hotelsFilePath, bookingsFilePath);
    }

    private void ProcessUserCommands(List<Hotel> hotels, List<Booking> bookings)
    {
        string input;
        while (!string.IsNullOrWhiteSpace(input = Console.ReadLine()))
        {
            var commandType = input.Split('(')[0];
            if (_commandHandlerRegistry.TryGetHandler(commandType, out var handler))
            {
                handler.Handle(input, hotels, bookings);
            }
            else
            {
                Console.WriteLine("Wrong command input! Try again.");
            }
        }
    }
}

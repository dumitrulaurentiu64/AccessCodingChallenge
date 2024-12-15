using HotelManagerChallenge.Commands;
using HotelManagerChallenge.Services;

namespace HotelManagerChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new JsonFileReader();
            var commandHandlerRegistry = new CommandHandlerRegistry();
            var hotelManager = new HotelManager(fileReader, commandHandlerRegistry);
            hotelManager.StartManager(args);
        }
    }
}

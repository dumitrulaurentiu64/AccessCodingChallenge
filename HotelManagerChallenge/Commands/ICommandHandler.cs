using HotelManagerChallenge.Models;

namespace HotelManagerChallenge.Commands
{
    public interface ICommandHandler
    {
        void Handle(string input, List<Hotel> hotels, List<Booking> bookings);
    }
}

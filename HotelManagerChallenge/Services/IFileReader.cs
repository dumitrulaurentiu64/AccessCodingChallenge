using HotelManagerChallenge.Models;

namespace HotelManagerChallenge.Services
{
    public interface IFileReader
    {
        List<Hotel> ReadHotels(string filePath);
        List<Booking> ReadBookings(string filePath);
    }
}

using HotelManagerChallenge.Models;
using HotelManagerChallenge.Utils;

namespace HotelManagerChallenge.Commands
{
    public class AvailabilityCommandHandler : ICommandHandler
    {
        public void Handle(string input, List<Hotel> hotels, List<Booking> bookings)
        {
            if (!ValidateInputFormat(input, out string hotelId, out string dateRange, out string roomTypeCode))
            {
                return;
            }

            List<DateTime> dates;
            if (!DateUtils.TryParseDateRange(dateRange, out dates))
            {
                return;
            }

            if (dates.Count == 0)
            {
                Console.WriteLine("Invalid date range format: {dateRange}. Expected format: yyyyMMdd or yyyyMMdd-yyyyMMdd.");
                return;
            }

            var hotel = hotels.FirstOrDefault(h => h.Id == hotelId);
            if (hotel == null)
            {
                Console.WriteLine("Hotel not found.");
                return;
            }

            var hotelBookings = bookings.Where(b => b.HotelId == hotelId).ToList();
            var availableRooms = CalculateAvailableRooms(hotel.Rooms, roomTypeCode, hotelBookings, dates);

            Console.WriteLine(availableRooms);
        }

        private int CalculateAvailableRooms(List<Room> rooms, string roomTypeCode, List<Booking> bookings, List<DateTime> dates)
        {
            int bookedRooms = 0;
            int totalRooms = rooms.Where(r => r.RoomType == roomTypeCode).Count();

            foreach (var booking in bookings)
            {
                if (booking.RoomType == roomTypeCode)
                {
                    foreach (var date in dates)
                    {
                        if (date >= booking.Arrival && date < booking.Departure)
                        {
                            bookedRooms++;
                            break;
                        }
                    }
                }
            }

            return totalRooms - bookedRooms;
        }

        private bool ValidateInputFormat(string input, out string hotelId, out string dateRange, out string roomTypeCode)
        {
            hotelId = null;
            dateRange = null;
            roomTypeCode = null;

            var parts = input.Replace("Availability(", "").Replace(")", "").Split(',');

            if (parts.Length < 3)
            {
                Console.WriteLine("Invalid input format. Expected: Availability(hotelId, dateRange, roomTypeCode)");
                return false;
            }

            hotelId = parts[0].Trim();
            dateRange = parts[1].Trim();
            roomTypeCode = parts[2].Trim();

            return true;
        }
    }
}

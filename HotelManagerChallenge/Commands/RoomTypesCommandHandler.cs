using HotelManagerChallenge.Models;
using HotelManagerChallenge.Utils;

namespace HotelManagerChallenge.Commands
{
    public class RoomTypesCommandHandler : ICommandHandler
    {
        public void Handle(string input, List<Hotel> hotels, List<Booking> bookings)
        {
            if (!ValidateInputFormat(input, out string hotelId, out string dateRange, out int numberOfPeople))
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

            var allocation = AllocateRooms(hotel, hotelBookings, dates, numberOfPeople);
            if (allocation == null)
            {
                Console.WriteLine("Allocation not possible!");
            }
            else
            {
                Console.WriteLine($"{hotel.Name}: {string.Join(", ", allocation)}");
            }
        }

        private List<string>? AllocateRooms(Hotel hotel, List<Booking> bookings, List<DateTime> dates, int numberOfPeople)
        {
            Dictionary<string, List<Room>> availableRooms = CheckAvailableRooms(hotel.Rooms, bookings, dates);
            var allocation = new List<string>();

            foreach (var roomType in hotel.RoomTypes.OrderByDescending(rt => rt.Size))
            {
                while (numberOfPeople >= roomType.Size && availableRooms.ContainsKey(roomType.Code) && availableRooms[roomType.Code].Count != 0)
                {
                    allocation.Add(roomType.Code);
                    availableRooms[roomType.Code].RemoveAt(0);
                    numberOfPeople -= roomType.Size;
                }
            }

            if (numberOfPeople > 0)
            {
                foreach (var roomType in hotel.RoomTypes.OrderByDescending(rt => rt.Size))
                {
                    if (availableRooms.ContainsKey(roomType.Code) && availableRooms[roomType.Code].Count != 0)
                    {
                        allocation.Add(roomType.Code + "!");
                        availableRooms[roomType.Code].RemoveAt(0);
                        numberOfPeople = 0;
                    }
                }
            }

            return numberOfPeople > 0 ? null : allocation;
        }

        private static Dictionary<string, List<Room>> CheckAvailableRooms(List<Room> rooms, List<Booking> bookings, List<DateTime> dates)
        {
            Dictionary<string, List<Room>> availableRooms = new Dictionary<string, List<Room>>();

            HashSet<Booking> processedBookings = new HashSet<Booking>();

            foreach (var room in rooms)
            {
                if (IsRoomAvailable(room, bookings, dates, processedBookings))
                {
                    if (!availableRooms.ContainsKey(room.RoomType))
                    {
                        availableRooms[room.RoomType] = new List<Room>();
                    }

                    availableRooms[room.RoomType].Add(room);
                }
            }

            return availableRooms;
        }

        private static bool IsRoomAvailable(Room room, List<Booking> bookings, List<DateTime> dates, HashSet<Booking> processedBookings)
        {
            foreach (var booking in bookings)
            {
                if (processedBookings.Contains(booking))
                {
                    continue;
                }

                if (booking.RoomType == room.RoomType)
                {
                    foreach (var date in dates)
                    {
                        if (date >= booking.Arrival && date < booking.Departure)
                        {
                            processedBookings.Add(booking);

                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool ValidateInputFormat(string input, out string hotelId, out string dateRange, out int numberOfPeople)
        {
            hotelId = null;
            dateRange = null;
            numberOfPeople = 0;

            var parts = input.Replace("RoomTypes(", "").Replace(")", "").Split(',');

            if (parts.Length < 3)
            {
                Console.WriteLine("Invalid input. Expected: RoomTypes(hotelId, dateRange, numberOfPeople)");
                return false;
            }

            hotelId = parts[0].Trim();
            dateRange = parts[1].Trim();

            if (!int.TryParse(parts[2].Trim(), out numberOfPeople))
            {

                Console.WriteLine("Invalid number of people. Please provide a valid positive number of persons.");
                return false;
            }

            if (numberOfPeople <= 0)
            {
                Console.WriteLine("Invalid number of people. Please provide a positive number of persons.");
                return false;
            }

            return true;
        }
    }
}

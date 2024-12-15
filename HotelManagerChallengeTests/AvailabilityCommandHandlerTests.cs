using HotelManagerChallenge.Commands;
using HotelManagerChallenge.Models;

namespace HotelManagerChallenge.Tests
{
    public class AvailabilityCommandHandlerTests
    {
        [Fact]
        public void Handle_ValidInput_PrintsAvailableRooms()
        {
            var handler = new AvailabilityCommandHandler();

            var hotels = new List<Hotel>
            {
                new Hotel { Id = "H1", Rooms = new List<Room>
                    {
                        new Room { RoomType = "SGL" },
                        new Room { RoomType = "SGL" },
                        new Room { RoomType = "DBL" }
                    }
                }
            };

            var bookings = new List<Booking>
            {
                new Booking { HotelId = "H1", RoomType = "SGL", Arrival = new DateTime(2024, 12, 15), Departure = new DateTime(2024, 12, 20) }
            };

            string input = "Availability(H1, 20241215-20241216, SGL)";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                handler.Handle(input, hotels, bookings);

                var output = sw.ToString().Trim();
                Assert.Equal("1", output);
            }
        }

        [Fact]
        public void Handle_InvalidInputFormat_PrintsErrorMessage()
        {
            var handler = new AvailabilityCommandHandler();

            var hotels = new List<Hotel>();
            var bookings = new List<Booking>();

            string input = "Availability(H1, qwerty)";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                handler.Handle(input, hotels, bookings);

                var output = sw.ToString().Trim();
                Assert.Equal("Invalid input format. Expected: Availability(hotelId, dateRange, roomTypeCode)", output);
            }
        }
    }
}

using HotelManagerChallenge.Commands;
using HotelManagerChallenge.Models;

namespace HotelManagerChallenge.Tests
{
    public class RoomTypesCommandHandlerTests
    {
        [Fact]
        public void Handle_ValidInput_AllocatesRoomsSuccessfully()
        {
            var handler = new RoomTypesCommandHandler();

            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    Id = "H1",
                    Name = "Test Hotel",
                    RoomTypes = new List<RoomType>
                    {
                        new RoomType { Code = "SGL", Size = 1 },
                        new RoomType { Code = "DBL", Size = 2 }
                    },
                    Rooms = new List<Room>
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

            string input = "RoomTypes(H1, 20241215-20241216, 3)";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                handler.Handle(input, hotels, bookings);

                var output = sw.ToString().Trim();
                Assert.Equal("Test Hotel: DBL, SGL", output);
            }
        }


        [Fact]
        public void Handle_InvalidNumberOfPeople_PrintsErrorMessage()
        {
            var handler = new RoomTypesCommandHandler();
            var hotels = new List<Hotel>();
            var bookings = new List<Booking>();

            string input = "RoomTypes(H1, 20241215-20241216, -1)";
 
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                handler.Handle(input, hotels, bookings);

                var output = sw.ToString().Trim();
                Assert.Equal("Invalid number of people. Please provide a positive number of persons.", output);
            }
        }
    }
}

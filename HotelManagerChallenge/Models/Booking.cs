using Newtonsoft.Json;
using System.Globalization;

namespace HotelManagerChallenge.Models
{
    public class Booking
    {
        public string HotelId { get; set; }
        public string RoomType { get; set; }
        public string RoomRate { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Arrival { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Departure { get; set; }
    }

    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "yyyyMMdd";

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                throw new JsonSerializationException("Null value cannot be converted a valid Date.");

            return DateTime.ParseExact(reader.Value.ToString(), DateFormat, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(DateFormat));
        }
    }
}

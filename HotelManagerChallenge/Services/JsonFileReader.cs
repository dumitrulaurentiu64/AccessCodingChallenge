using HotelManagerChallenge.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace HotelManagerChallenge.Services
{
    public class JsonFileReader : IFileReader
    {
        public List<Hotel> ReadHotels(string filePath)
        {
            string dataDirectory = $"{Directory.GetCurrentDirectory()}/Data/";

            try
            {
                string jsonContent = File.ReadAllText($"{dataDirectory}/{filePath}");
                string jsonContentSanitized = SanitizeJson(jsonContent);

                return JsonConvert.DeserializeObject<List<Hotel>>(jsonContentSanitized);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing file {filePath}", ex.Message);
                return new List<Hotel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while reading file {filePath}", ex.Message);
                return new List<Hotel>();
            }
        }

        public List<Booking> ReadBookings(string filePath)
        {
            string dataDirectory = $"{Directory.GetCurrentDirectory()}/Data/";

            try
            {
                return JsonConvert.DeserializeObject<List<Booking>>(File.ReadAllText($"{dataDirectory}/{filePath}"));
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"Error deserializing file {filePath}", ex.Message);
                return new List<Booking>(); 
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while reading {filePath}", ex.Message);
                return new List<Booking>();
            }
        }

        private static string SanitizeJson(string jsonContent)
        {
            string jsonContentWithDoubleQuotes = jsonContent.Replace('“', '"').Replace('”', '"');

            string jsonContentWithCommas = Regex.Replace(jsonContentWithDoubleQuotes, @"(""size"":\s*\d)(?=\s*""description"")", "$1,");

            return jsonContentWithCommas;
        }
    }
}

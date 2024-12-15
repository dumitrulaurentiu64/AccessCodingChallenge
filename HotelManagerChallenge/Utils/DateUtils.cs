namespace HotelManagerChallenge.Utils
{
    public static class DateUtils
    {
        public static bool TryParseDateRange(string dateRange, out List<DateTime> dates)
        {
            dates = null;

            try
            {
                dates = ParseDateRange(dateRange);
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Invalid date range format: {dateRange}. Expected format: yyyyMMdd or yyyyMMdd-yyyyMMdd.");
                return false;
            }
        }

        private static List<DateTime> ParseDateRange(string dateRange)
        {
            try
            {
                if (dateRange.Contains('-'))
                {
                    var dates = dateRange.Split('-');
                    var startDate = DateTime.ParseExact(dates[0], "yyyyMMdd", null);
                    var endDate = DateTime.ParseExact(dates[1], "yyyyMMdd", null);

                    if (endDate < startDate)
                    {
                        return new List<DateTime>();
                    }

                    int numberOfDays = (endDate - startDate).Days + 1;

                    return Enumerable.Range(0, numberOfDays)
                                     .Select(day => startDate.AddDays(day))
                                     .ToList();
                }
                else
                {
                    var date = DateTime.ParseExact(dateRange, "yyyyMMdd", null);
                    return new List<DateTime> { date };
                }
            }
            catch
            {
                return new List<DateTime>();
            }
        }
    }
}

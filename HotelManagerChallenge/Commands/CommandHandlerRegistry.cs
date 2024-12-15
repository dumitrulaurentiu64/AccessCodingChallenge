namespace HotelManagerChallenge.Commands
{
    public class CommandHandlerRegistry : ICommandHandlerRegistry
    {
        private readonly Dictionary<string, ICommandHandler> _handlers = new()
        {
            { "Availability", new AvailabilityCommandHandler() },
            { "RoomTypes", new RoomTypesCommandHandler() }
        };

        public bool TryGetHandler(string commandType, out ICommandHandler handler) =>
            _handlers.TryGetValue(commandType, out handler);
    }
}

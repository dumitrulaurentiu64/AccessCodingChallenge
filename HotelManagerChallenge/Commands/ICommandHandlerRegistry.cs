namespace HotelManagerChallenge.Commands
{
    public interface ICommandHandlerRegistry
    {
        bool TryGetHandler(string commandType, out ICommandHandler handler);
    }
}

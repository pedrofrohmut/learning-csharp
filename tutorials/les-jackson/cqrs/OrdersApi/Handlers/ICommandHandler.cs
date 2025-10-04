public interface ICommandHandler<TCommand> where TCommand : notnull
{
    Task HandleAsync(TCommand command);
}


public class InProcessEventPublisher : IEventPublisher
{
    private readonly IServiceProvider serviceProvider;

    public InProcessEventPublisher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TEvent>(TEvent evt)
    {
        using var scope = this.serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
        foreach (var handler in handlers) {
            await handler.HandleAsync(evt);
        }
    }
}

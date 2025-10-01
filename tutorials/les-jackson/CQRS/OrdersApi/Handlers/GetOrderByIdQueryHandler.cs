using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQueryHandler
{
    public static async Task<Order?> Handle(ApplicationDbContext dbContext, GetOrderByIdQuery query)
    {
        return await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == query.orderId);
    }
}

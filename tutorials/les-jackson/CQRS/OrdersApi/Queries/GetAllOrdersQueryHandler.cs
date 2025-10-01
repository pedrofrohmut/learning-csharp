using Microsoft.EntityFrameworkCore;

public class GetAllOrdersQueryHandler
{
    public static async Task<List<Order>> Handle(ApplicationDbContext dbContext)
    {
        return await dbContext.Orders.ToListAsync();
    }
}

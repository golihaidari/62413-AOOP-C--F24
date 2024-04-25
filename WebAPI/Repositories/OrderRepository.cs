using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPI.Contexts;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class OrderRepository(ApiDbContext dbContext) : IRepository<Order>
    {
        public async Task<IEnumerable<Order>?> GetAll()
        {
            var orders = await dbContext.Orders
                                 .Include(o => o.OrderDetails)
                                 .ThenInclude(od => od.Product).ToListAsync();
            if (orders.Count > 0)
            {
                return orders;
            }
            else
            {
                return null;
            }
        }

        public async Task<Order?> Get(string orderId)
        {            
            var orders = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == new Guid(orderId));
            if (orders == null) { return null; }
            else
            {
                return await dbContext.Orders
                .Include(o => o.CustomerEmail)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == new Guid(orderId));
            }            
        }


        public async Task<bool> Create(Order order)
        {
            dbContext.Add(order);
            int created = await dbContext.SaveChangesAsync();

            if (created > 0) { return true; }
            else { return false; }
        }
        
        public async Task<bool> Update(Order order)
        {
            dbContext.Update(order);
            int updated = await dbContext.SaveChangesAsync();

            if (updated > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Delete(string orderId)
        {
            var order = dbContext.Orders.Find(new Guid(orderId));
            int deleted = 0;
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                deleted = await dbContext.SaveChangesAsync();
            }

            if (deleted > 0 || order == null) { return true; }
            else { return false; }
        }
    }
}

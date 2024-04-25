using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Models;
using static NuGet.Packaging.PackagingConstants;

namespace WebAPI.Repositories
{
    public class OrderDetailRepository(ApiDbContext dbContext) : IRepository<OrderDetail>
    {
        public async Task<IEnumerable<OrderDetail>?> GetAll()
        {
            var orerDetails= await dbContext.OrderDetails.ToListAsync();
            if (orerDetails.Count > 0)
            {
                return orerDetails;
            }
            else
            {
                return null;
            }
        }

        public async Task<OrderDetail?> Get(string id)
        {
            return await dbContext.OrderDetails.FirstOrDefaultAsync(od => od.Id == new Guid(id));
        }

        public async Task<bool> Create(OrderDetail orderDetail)
        {
            dbContext.Add(orderDetail);
            int created = await dbContext.SaveChangesAsync();

            if (created > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Update(OrderDetail orderDetail)
        {
            dbContext.Update(orderDetail);
            int updated = await dbContext.SaveChangesAsync();

            if (updated > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Delete(string orderDetailId)
        {
            var orderDetail = dbContext.OrderDetails.Find(orderDetailId);
            int deleted = 0;
            if (orderDetail != null)
            {
                dbContext.OrderDetails.Remove(orderDetail);
                deleted = await dbContext.SaveChangesAsync();
            }

            if (deleted > 0 || orderDetail == null) { return true; }
            else { return false; }
        }
    }
}

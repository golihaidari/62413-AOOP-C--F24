using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using WebAPI.Contexts;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ProductRepository(ApiDbContext dbContext) : IRepository<Product>
    {
        public async Task<IEnumerable<Product>?> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> Get(string id)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> Create(Product product)
        {
            dbContext.Add(product);
            int created = await dbContext.SaveChangesAsync();

            if (created > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Update(Product product)
        {
            dbContext.Update(product);
            int updated = await dbContext.SaveChangesAsync();

            if (updated > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Delete(string productId)
        {
            var product = dbContext.Products.Find(productId);
            int deleted = 0;
            if (product != null)
            {
                dbContext.Products.Remove(product);
                deleted = await dbContext.SaveChangesAsync();
            }

            if (deleted > 0 || product == null) { return true; }
            else { return false; }
        } 
    }
}

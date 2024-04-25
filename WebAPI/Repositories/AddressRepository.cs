using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class AddressRepository(ApiDbContext dbContext) : IRepository<Address>
    {
        public async Task<IEnumerable<Address>?> GetAll()
        {
            return await dbContext.Addresses.ToListAsync();
        }

        public async Task<Address?> Get(string email)
        {
            return await dbContext.Addresses.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<bool> Create(Address address)
        {
            dbContext.Add(address);
            int created = await dbContext.SaveChangesAsync();

            if (created > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Update(Address address)
        {
            var existingAddress = dbContext.ChangeTracker.Entries<Address>().FirstOrDefault(e => e.Entity.Email == address.Email);
            if (existingAddress != null)
            {
                // Entity with the same key value is already being tracked, handle accordingly
                dbContext.Entry(existingAddress.Entity).State = EntityState.Detached;
            }
            dbContext.Update(address);
            int updated = await dbContext.SaveChangesAsync();

            if (updated > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Delete(string Email)
        {
            var address = dbContext.Addresses.Find(Email);
            int deleted = 0;
            if (address != null)
            {
                dbContext.Addresses.Remove(address);
                deleted = await dbContext.SaveChangesAsync();
            }

            if (deleted > 0 || address == null) { return true; }
            else { return false; }
        }      
    }
}

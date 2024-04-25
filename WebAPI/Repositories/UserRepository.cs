using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRepository(ApiDbContext dbContext) : IRepository<User>
    {
        public async Task<IEnumerable<User>?> GetAll()
        {
            return await dbContext.Users.ToListAsync(); 
        }        

        public async Task<User?> Get(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> Create(User user)
        {
            dbContext.Add(user);
            int created = await dbContext.SaveChangesAsync();

            if (created > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Update(User user)
        {
            dbContext.Update(user);
            int updated = await dbContext.SaveChangesAsync();

            if (updated > 0) { return true; }
            else { return false; }
        }

        public async Task<bool> Delete(string email)
        {
            var users = dbContext.Users.Find(email);
            int deleted = 0;
            if (users != null)
            {
                dbContext.Users.Remove(users);
                deleted = await dbContext.SaveChangesAsync();  
            }

            if(deleted > 0 || users == null) { return true; }
            else { return false; }
        }        
    }
}

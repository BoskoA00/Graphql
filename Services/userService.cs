using Graphql_server.Interfaces;
using Graphql_server.Types;
using Microsoft.EntityFrameworkCore;

namespace Graphql_server.Services
{
    public class UserService : userInterface
    {
        private readonly IDbContextFactory<DatabaseContext> _databaseContext;
        public UserService(IDbContextFactory<DatabaseContext> contextFactory )
        {
            _databaseContext = contextFactory;
        }

        public async Task<string> CreateUser(User user)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return "da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteUser(User user)
        {
            try
            {
                if (user != null)
                {
                    var db = _databaseContext.CreateDbContext();
                    db.Users.Remove(user);
                    await db.SaveChangesAsync();
                    return "Da";

                }
                else
                {
                    return "Ne";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<User?> getUserById(int id)
        {

            var db = _databaseContext.CreateDbContext();
            return await db.Users.Where( u => u.Id == id).Include( u => u.Questions).Include( u => u.Answers).Include(u => u.Oglasi).FirstOrDefaultAsync();
        }

        public async Task<List<User>?> getUsers()
        {
            var db = _databaseContext.CreateDbContext();
            return await db.Users.Include( u => u.Oglasi).Include( u => u.Questions).Include( u => u.Answers).ToListAsync();
        }

        public async Task<string> UpdateUser(User user)
        {
            try
            {

            var db = _databaseContext.CreateDbContext();
            
            User? userToUpdate = await db.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();
            if (userToUpdate == null)
            {
                return "Ne"; 
            }
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.ImageName = user.ImageName;
            userToUpdate.Role = user.Role;
            
            await db.SaveChangesAsync();
            return "da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }
}

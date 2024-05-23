using Graphql_server.Interfaces;
using Graphql_server.Types;
using Microsoft.EntityFrameworkCore;

namespace Graphql_server.Services
{
    public class oglasService : oglasInterface
    {
        private readonly IDbContextFactory<DatabaseContext> _databaseContext;
        public oglasService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            _databaseContext = contextFactory;
        }

        public async Task<string> CreateOglas(Oglas oglas)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();

                db.Oglasi.Add(oglas);
                await db.SaveChangesAsync();
                return "da";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteOglas(Oglas oglas)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();


                db.Oglasi.Remove(oglas);
                await db.SaveChangesAsync();
                return "da";
            }
            catch (Exception ex) { return ex.Message; }
        }

        public async Task<List<Oglas>?> GetOglasi()
        {
            var db = _databaseContext.CreateDbContext();

            return await db.Oglasi.Include(x => x.User).ToListAsync();
        }

        public async Task<Oglas?> GetOglasById(int id)
        {
            var db = _databaseContext.CreateDbContext();

            return await db.Oglasi.Where(o => o.Id == id).Include(x => x.User).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateOglas(Oglas oglas)
        {
            try
            {
                var db = _databaseContext.CreateDbContext();

                Oglas? oglasToUpdate = await db.Oglasi.Where(o => o.Id == oglas.Id).FirstOrDefaultAsync();
                if (oglasToUpdate == null)
                {
                    return "ne";
                }
                oglasToUpdate.Title = oglas.Title;
                oglasToUpdate.City = oglas.City;
                oglasToUpdate.Country = oglas.Country;
                oglasToUpdate.Price = oglas.Price;
                oglasToUpdate.Size = oglas.Size;
                oglasToUpdate.PicturePath = oglas.PicturePath;
                oglasToUpdate.Type = oglas.Type;

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

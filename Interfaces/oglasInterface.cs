using Graphql_server.Types;
using System.Net;

namespace Graphql_server.Interfaces
{
    public interface oglasInterface
    {

        Task<Oglas?> GetOglasById(int id);
        Task<List<Oglas>?> GetOglasi();
        Task<string> DeleteOglas(Oglas oglas);
        Task<string> CreateOglas(Oglas oglas);
        Task<string> UpdateOglas(Oglas oglas);
    }
}

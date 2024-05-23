using Graphql_server.Types;

namespace Graphql_server.Interfaces
{
    public interface userInterface
    {
        Task<User?> getUserById(int id);
        Task<List<User>?> getUsers();
        Task<string> CreateUser(User user);
        Task<string> DeleteUser(User user);
        Task<string> UpdateUser(User user);
    }
}

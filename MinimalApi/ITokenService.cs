using DataAccess.Models;
namespace MinimalApi
{
    public interface ITokenService
    {
        string BuildToken(User user);
    }
}

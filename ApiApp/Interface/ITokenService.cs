using ApiApp.Entities;

namespace ApiApp.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}

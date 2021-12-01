using Gl.Core.Domain;

namespace Gl.Manager.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
using System.IdentityModel.Tokens.Jwt;

namespace NoGravity.Data.DataServices
{
    public interface IJWTService
    {
        string GenerateToken(int id);
        JwtSecurityToken Verify(string jwt);
    }
}
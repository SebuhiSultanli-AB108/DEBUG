using DEBUG.Core.Entities;

namespace DEBUG.BL.ExternalServices;

public interface IJWTTokenHandler
{
    string CreateToken(User user, int hours);
}
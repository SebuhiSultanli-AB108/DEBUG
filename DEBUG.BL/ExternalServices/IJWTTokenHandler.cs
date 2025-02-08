using DEBUG.Core.Models;

namespace DEBUG.BL.ExternalServices;

public interface IJWTTokenHandler
{
    string CreateToken(User user, int hours);
}
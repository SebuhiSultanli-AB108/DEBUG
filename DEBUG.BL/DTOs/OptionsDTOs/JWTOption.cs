namespace DEBUG.BL.DTOs.OptionsDTOs;

public class JWTOption
{
    public const string Jwt = "JwtOptions";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}
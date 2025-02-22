namespace DEBUG.BL.DTOs.OptionsDTOs;

public class SmtpOption
{
    public const string Smtp = "SmtpOptions";
    public string Host { get; set; }
    public int Port { get; set; }
    public string Email { get; set; }
    public string SmtpKey { get; set; }
    public string Name { get; set; }
}
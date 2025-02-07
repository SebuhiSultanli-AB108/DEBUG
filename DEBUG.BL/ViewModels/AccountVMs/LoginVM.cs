using System.ComponentModel.DataAnnotations;

namespace DEBUG.BL.ViewModels.AccountVMs;

public class LoginVM
{
    [MaxLength(128), Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [MaxLength(128), Required, DataType(DataType.Password)]
    public string Password { get; set; }
}
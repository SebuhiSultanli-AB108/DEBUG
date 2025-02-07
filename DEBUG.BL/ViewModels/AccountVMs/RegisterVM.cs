using System.ComponentModel.DataAnnotations;

namespace DEBUG.BL.ViewModels.AccountVMs;

public class RegisterVM
{
    [MaxLength(64), Required]
    public string FullName { get; set; }
    [MaxLength(32), Required]
    public string UserName { get; set; }
    [MaxLength(128), Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [MaxLength(128), Required, DataType(DataType.Password)]
    public string Password { get; set; }
    public bool HasAcceptedTerms { get; set; }
}
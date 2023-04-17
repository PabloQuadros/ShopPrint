using ShopPrint_API.Entities.Validation.User;

namespace ShopPrint_API.Entities.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        [UserNameValidation]
        public string? UserName { get; set; }
        [EmailValidation]
        public string? Email { get; set; }
        [PasswordValidation]
        public string? Password { get; set; }
        [PhoneNumberValidation]
        public string? PhoneNumber { get; set; }
        [RoleValidation]
        public string? Role { get; set; }
    }
}

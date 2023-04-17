using ShopPrint.Domain.Validation.Shared;
using ShopPrint.Domain.Validation.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Application.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        [NameValidation]
        public string? UserName { get; set; }
        [UserEmailValidation]
        public string? Email { get; set; }
        [PasswordValidation]
        public string? Password { get; set; }
        [UserPhoneNumberValidation]
        public string? PhoneNumber { get; set; }
        [UserRoleValidation]
        public string? Role { get; set; }
    }
}

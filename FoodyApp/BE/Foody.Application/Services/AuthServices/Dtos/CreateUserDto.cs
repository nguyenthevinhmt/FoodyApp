using Foody.Domain.Constants;
using Foody.Share.Shared;
using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.AuthServices.Dtos
{
    public class CreateUserDto
    {
        private string _email;

        [Required]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim();
        }
        private string _password;
        [Required]
        public string Password
        {
            get => _password;
            set => _password = value?.Trim();
        }
        [IntegerRange(AllowableValues = new int[] { UserTypes.Admin, UserTypes.Customer })]
        public int UserType { get; set; }

    }
}

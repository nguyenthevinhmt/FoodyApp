using Foody.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Foody.Domain.Entities
{
    public class User : BaseEntity<int>, ICreated
    {
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [NotMapped] 
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(250)]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<UserAddress> UserAddresses { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
    }
}

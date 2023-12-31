﻿using Foody.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(250)]
        [EmailAddress(ErrorMessage = "Email sai định dạng")]
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}

using System;
using Microsoft.AspNetCore.Identity;

namespace CmentarzKomunalny.Web.Models.Cmentarz
{
    public class User // : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;

namespace UserManagement.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(string userName) : base(userName)
        {

        }
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public DateTime? BirthDate { set; get; }
        public byte? GenderId { set; get; }
        public Gender Gender { set; get; }
        public string Address { set; get; }
        public DateTime? CreatedOn { set; get; } = DateTime.Now;
        public int? AttachmentId { set; get; }
        public DateTime? LastLoginDate { set; get; }
    }
}

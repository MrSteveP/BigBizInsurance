using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Mapper;
using UserManagement.Domain.Models;

namespace UserManagement.Domain.ViewModels
{
    public class UserViewModel
    {
        public Guid? Id { set; get; }

        [Required]
        public string FirstName { set; get; }

        [Required]
        public string MiddleName { set; get; }

        [Required]
        public string LastName { set; get; }

        public DateTime? BirthDate { set; get; }

        [Required]
        public string BirthDateString { set; get; }

        [Required]
        public byte? GenderId { set; get; }
        public string Address { set; get; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { set; get; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { set; get; }

        public bool Active { set; get; }

        public IFormFile File { set; get; }

        public List<string> Roles { set; get; } = new List<string>();

        public static implicit operator ApplicationUser(UserViewModel model)
        {
            return UserMapper.MapToModel(model);
        }

        public static implicit operator UserViewModel(ApplicationUser user)
        {
            return UserMapper.MapToViewModel(user);
        }
    }
}

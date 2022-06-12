﻿using System;
using System.ComponentModel.DataAnnotations;
namespace Todoapp.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is mandatory.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is mandatory.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm the password.")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}


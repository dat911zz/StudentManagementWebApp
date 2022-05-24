﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagementWebApp.Models
{
    public class User
    {
        #region Properties     
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUser { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }


        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Hash { get; set; }
        public bool Manager { get; set; }
        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }
        public string FullName_VN()
        {
            return this.LastName + " " + this.FirstName;
        }
        #endregion
        #region Constructors
        public User() 
        {
            idUser = 0;
        }
        /// <summary>
        /// For Register and Login
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="manager"></param>
        public User(int idUser, string firstName, string lastName, string email, string userName, string password, string confirmPassword, bool manager)
        {
            this.idUser = idUser;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
            Manager = manager;
        }
        /// <summary>
        /// For Get data form DB with Encrypting Password
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="hash">Encrypting Password</param>
        /// <param name="manager"></param>
        public User(string firstName, string lastName, string email, string userName, string hash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            Hash = hash;
        }
        public User(User x)
        {
            this.idUser = x.idUser;
            this.FirstName = x.FirstName;
            this.LastName = x.LastName;
            this.Email = x.Email;
            this.UserName = x.UserName;
            this.Password = x.Password;
            this.ConfirmPassword = x.ConfirmPassword;
            this.Manager = x.Manager;
        }
        #endregion
    }
}
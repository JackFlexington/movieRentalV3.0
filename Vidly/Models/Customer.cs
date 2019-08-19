/*
    Author: Jacob Storer
    Date: 08/19/2019
    Description: Movie Rental Application
    Framework: ASP.NET
    Uses: Razor, MVC, Entity Framework, MySQL Database, Lambda Expressions, Form Markup, 
        CRUD operations.
*/

/* Imports */
using System;
// using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Don't forget to add
// using System.Linq;
// using System.Web;

namespace Vidly.Models
{
    // Initialize structure for Customer -- Add setters & getters
    public class Customer
    {
        // Class Structure
        public int Id { get; set; } // key for Customer class

        [Required] // Name column is no longer nullable
        [StringLength(255)] // Max length of Name column
        public string Name { get; set; } // Customer Name

        public bool IsSubscribedToNewsLetter { get; set; } // Subscribed?

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } // Foreign key

        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; } // Birthdate -- Optional (Nullification)

        // Class within Class
        public MembershipType MembershipType { get; set; } // Membership Class
    }
}
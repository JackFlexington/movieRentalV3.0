/*
    Author: Jacob Storer
    Date: 08/19/2019
    Description: Movie Rental Application
    Framework: ASP.NET
    Uses: Razor, MVC, Entity Framework, MySQL Database, Lambda Expressions, Form Markup, 
        CRUD operations.
*/

/* Imports */
// using System;
using System.Collections.Generic;
// using System.Linq;
// using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    // Structure used to house data 
    public class MovieFormViewModel
    {
        public IEnumerable<GenreType> GenreTypes { get; set; } // Iterative "list" of GenreTypes
        public Movie Movie { get; set; } // Re-use Entity - because its easier. (Not recommended in large-scale applications that contain multiple domains.)
    }
}
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
using System.Data.Entity; // Don't forget to add this
// using System.Collections.Generic;
using System.Linq;
// using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // Declare type of DbContext for variable _context
        private ApplicationDbContext _context; // Gateway to access Database

        public MoviesController()
        {
            _context = new ApplicationDbContext(); // Assign DbContext to gateway
        } // End -- MoviesController()

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); // _context is disposable.. so dispose of it
        } // End -- Dispose()

        public ActionResult AddMovie()
        {
            // Input: none;
            // Processing: Get GenreTypes listed in database; initialize new Object of MovieFormViewModel;
                // Assign genre types that already exist in database to new Object;
            // Output: Display view MovieForm.cshtml; Pass genre types to view;

            var genreTypes = _context.GenreTypes.ToList(); //  Acquire all genre types; Executes immediately;
            var viewModel = new MovieFormViewModel // Initialize new Object class
            {
                GenreTypes = genreTypes // Assign list of genre types to viewModel.GenreTypes
            };

            return View("MovieForm", viewModel); // Call MovieForm.cshtml; Pass Object viewModel
        } // End -- AddMovie()

        [HttpPost] // Called only with POST, not GET
        public ActionResult Save(Movie movie)
        {
            // Input: Movie Object;
            // Processing: Determine if movie is non-existent;
                // If true, add current time & prime the desired additions for specified movie database;
                // If false, prime the desired changes for specified movie in database;
            // Output: Commit changes to database; Redirect user to index of movies ( URL: ~/Movies/ );

            // New movie?
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now; // Set current time for when movie is added to inventory
                _context.Movies.Add(movie); // Primes transaction used later to persist the changes
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id); // "Single" = Throws exception if duplicate movie is found

                // Assign values acquired from POST Movie form to be altered in database.
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreTypeId = movie.GenreTypeId;
                movieInDb.NumberInStock = movie.NumberInStock;
            } // End -- else

            _context.SaveChanges(); // Commit changes
            return RedirectToAction("index", "Movies"); // Redirect user to index of "movies" sub directory
        } // End -- Create()

        public ActionResult Edit(int id)
        {
            // Input: id - used for determining specified movie;
            // Processing: Pull values of specified movie from database;
                // Initialize new Object used to hold values from database; Append GenreTypes;
            // Output: Display view MovieForm; Pass Object viewModel to view;

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id); // Display only one record or default value; Evaluate(Movies.Id == id)

            // Movie found?
            if (movie == null)
                return HttpNotFound(); // Movie not found

            var viewModel = new MovieFormViewModel // Initialize MovieFormViewModel object
            {
                Movie = movie, // Get Movie value;
                GenreTypes = _context.GenreTypes.ToList() // Get GenreType values; Executes immediately;
            };

            return View("MovieForm", viewModel); // Override so MVC doesn't look for "Edit"; pass viewModel;
        } // End -- ActionResult Edit()

        // Route Constraints
        [Route("movies/details/{id}")] // Looking for any value at {id}

        // Get target movies values
        public ActionResult Details(int id)
        {
            // Input: id - used for determining specified movie;
            // Processing: Pull values of specified movie from database;
                // Initialize new Object used to hold values from database; Append GenreTypes;
            // Output: Call view Details; Pass Object movie to view; ( URL: ~/Movies/Details/{id} );

            var movie = _context.Movies.Include(m => m.GenreType).SingleOrDefault(m => m.Id == id); // Append Class of MembershipType to movies; Display only one record or default value; Evalute (movies.Id == id)

            if (movie == null) // Default value passed back?
                return HttpNotFound(); // Movie not found

            return View(movie); // Return context of specific movie
        } // End -- ActionResult Details()

        // Get listing of Movies 
        public ActionResult Index()
        {
            // Input: None;
            // Processing: Get values from database of Movies table; Append GenreTypes to query;
            // Output: Context of Movies table in database ( URL: ~/Movies/ )

            var movies = _context.Movies.Include(m => m.GenreType).ToList(); // Append Class of GenreType to movies; Executes immediately;

            return View(movies); // Return context of database
        } // End -- ActionResult Index()
    } // End -- MoviesController : Controller
} // End -- namespace Vidly.Controllers
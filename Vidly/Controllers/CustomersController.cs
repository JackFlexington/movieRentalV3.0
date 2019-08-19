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
using System.Data.Entity; // Don't forget to add this
// using System.Collections.Generic;
using System.Linq;
// using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // Declare type of DbContext for variable _context
        private ApplicationDbContext _context; // Gateway to access Database

        public CustomersController()
        {
            _context = new ApplicationDbContext(); // Assign DbContext to gateway
        } // End -- CustomersController()

        protected override void Dispose(bool disposing)
        {
            _context.Dispose(); // _context is disposable.. so dispose of it
        } // End -- Dispose(bool disposing)

        public ActionResult New()
        {
            // Input: none;
            // Processing: Get MembershipTypes listed in database; initialize new Object of CustomerFormViewModel;
                // Assign membership types that already exist in database to new Object;
            // Output: Display view CustomerForm.cshtml; Pass membership types to view;

            var membershipTypes = _context.MembershipTypes.ToList(); //  Executes immediately;
            var viewModel = new CustomerFormViewModel // Initialize new Object class
            {
                MembershipTypes = membershipTypes // Assign list of membership types to viewModel.MembershipTypes
            };

            return View("CustomerForm", viewModel); // Call CustomerForm.cshtml; Pass Object viewModel
        } // End -- New()

        [HttpPost] // Called only with POST, not GET
        public ActionResult Save(Customer customer) // Binds model to request data
        {
            // Input: Customer Object;
            // Processing: Determine if customer is non-existent;
                // If true, prime the desired additions for specified customer in database;
                // If false, prime the desired changes for specified customer in database;
            // Output: Commit changes to database; Redirect user to index of customers ( URL: ~/Customer/ );

            // New customer?
            if (customer.Id == 0)
                _context.Customers.Add(customer); // Primes transaction used later to persist the changes
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id); // "Single" = Throws exception if duplicate customer is found

                // Assign values acquired from POST Customer form to be altered in database.
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            } // End -- else

            _context.SaveChanges(); // Commit changes
            return RedirectToAction("index", "Customers"); // Redirect user to index of "customers" sub directory
        } // End -- Create()

        public ActionResult Edit(int id)
        {
            // Input: id - used for determining specified customer;
            // Processing: Pull values of specified customer from database;
                // Initialize new Object used to hold values from database; Append MembershipTypes;
            // Output: Display view CustomerForm; Pass Object viewModel to view;

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id); // Display only one record or default value; Evaluate(Customers.Id == id)

            // Customer found?
            if (customer == null)
                return HttpNotFound(); // Customer not found

            var viewModel = new CustomerFormViewModel // Initialize CustomerFormViewModel object
            {
                Customer = customer, // Get Customer value;
                MembershipTypes = _context.MembershipTypes.ToList() // Get MembershipType values; Executes immediately;
            };

            return View("CustomerForm", viewModel); // Call CustomerForm.cshtml; Pass Object viewModel
        } // End -- ActionResult Edit()

        // Route Constraints
        [Route("customers/details/{id}")] // Looking for any value at {id}

        // Get target customers values
        public ActionResult Details(int id)
        {
            // Input: id - used for determining specified customer;
            // Processing: Pull values of specified movie from database;
                // Initialize new Object used to hold values from database; Append MembershipTypes;
            // Output: Context about specific customer ( URL: ~/Customers/Details/{id} );

            /* Notes: id = target appropriate customer name */

            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id); // Append Class of MembershipType to customers; Display only one record or default value; Evalute (Customers.Id == id)

            if (customer == null) // Default value passed back?
                return HttpNotFound(); // Customer not found

            return View(customer); // Return context of specific customer
        } // End -- ActionResult Details()

        // Get listing of Customers 
        public ActionResult Index()
        {
            // Input: None;
            // Processing: Get values from database of Customers table; Append MembershipTypes to query;
            // Output: Context of Customers table in database ( URL: ~/Customers/ )

            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); // Append Class of MembershipType to customers; Executes immediately;

            return View(customers); // Return context of database
        } // End -- ActionResult Index()
    } // End -- CustomersController : Controller
} // End -- namespace Vidly.Controllers
﻿/*
    Author: Jacob Storer
    Date: 08/19/2019
    Description: Movie Rental Application
    Framework: ASP.NET
    Uses: Razor, MVC, Entity Framework, MySQL Database, Lambda Expressions, Form Markup, 
        CRUD operations.
*/

/* Imports */
// using System.Web;
using System.Web.Optimization;

namespace Vidly
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css", // Added external stylin' sheet -- Custom edits inside ~js
                      "~/Content/site.css"));
        }
    }
}

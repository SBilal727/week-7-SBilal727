using ClassSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace ClassSchedule.Components
{
    // #24. Add a new class named DayFilter that inherits the ViewComponent class.
    public class DayFilter : ViewComponent
    {
        // #25. Update the class to receive an IRepository<Day> object via dependency injection.
        private IRepository<Day> days { get; set; }
        public DayFilter(IRepository<Day> rep) => days = rep;

        // #26. Add an Invoke() method that returns an IViewComponentResult object.
        public IViewComponentResult Invoke()
        {

            // #27. Add code to the Invoke() method that uses the IRepository<Day> object to
            //      get a collection of Day objects sorted by DayId.Use the View() method to
            //      pass this collection to the partial view.

            var options = new QueryOptions<Day>
            {
                OrderBy = d => d.DayId
            };

            return View(days.List(options));
        }
    }
}
using ClassSchedule.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClassSchedule.Controllers
{
    public class HomeController : Controller
    {

        


        private IClassScheduleUnitOfWork unitOfWork { get; set; }

        // #9. Open the Home controller and add a parameter to its constructor of the
        // IHttpContextAccessor type.Store this parameter value in a private property.
        private IHttpContextAccessor httpContextAccessor { get; set; }

        // Update constructor

        public HomeController(IClassScheduleUnitOfWork unitWork, IHttpContextAccessor accessor)
        {
            unitOfWork = unitWork;
            httpContextAccessor = accessor; 
        }


        public ViewResult Index(int id)
        {
            // options for Days query
            var dayOptions = new QueryOptions<Day> {
                OrderBy = d => d.DayId
            };

            // options for Classes query
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day"
            };


           

            if (id == 0) {
                classOptions.OrderBy = c => c.DayId;
                classOptions.ThenOrderBy = c => c.MilitaryTime;
            }
            else {
                classOptions.Where = c => c.DayId == id;
                classOptions.OrderBy = c => c.MilitaryTime;
            }

            // execute queries
            var dayList = unitOfWork.Days.List(dayOptions);
            var classList = unitOfWork.Classes.List(classOptions);

            // send data to view
            ViewBag.Id = id;
            ViewBag.Days = dayList;
            return View(classList);
        }
    }
}
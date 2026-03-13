using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Class> classes { get; set; }
    

        public HomeController(IRepository<Class> classRep, IRepository<Day> daysRep)
        {
            classes = classRep;
     
        }

        public ViewResult Index(int id)
        {
            

            // options for Classes query
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day"
            };

            // order classes by day and then time on first load (ie, there's no filter value).
            // Otherwise, filter by day and order by time.
            if (id == 0) {
                classOptions.OrderBy = c => c.DayId;
                classOptions.ThenOrderBy = c => c.MilitaryTime;
            }
            else {
                classOptions.Where = c => c.DayId == id;
                classOptions.OrderBy = c => c.MilitaryTime;
            }

        
            var classList = classes.List(classOptions);

            return View(classList);
        }
    }
}
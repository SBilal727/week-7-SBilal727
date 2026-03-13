using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class ClassController : Controller
    {
        
        private IClassScheduleUnitOfWork unitOfWork { get; set; }

        // #11
        private IHttpContextAccessor httpContextAccessor { get; set; }

        // Update constructor


        public ClassController(IClassScheduleUnitOfWork unitWork, IHttpContextAccessor accessor)
        {
            unitOfWork = unitWork;
            httpContextAccessor = accessor;
        }

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            this.LoadViewBag("Add");
            return View("AddEdit", new Class());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var c = this.GetClass(id);
            return View("AddEdit", c);
        }

        [HttpPost]
        public IActionResult Add(Class c)
        {
            bool isAdd = c.ClassId == 0;

            if (ModelState.IsValid) {
                if (isAdd)
                    unitOfWork.Classes.Insert(c);
                else
                    unitOfWork.Classes.Update(c);
                unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            else {
                string operation = (isAdd) ? "Add" : "Edit";
                this.LoadViewBag(operation);
                return View("AddEdit", c);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var c = this.GetClass(id);
            return View(c);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Class c)
        {
            unitOfWork.Classes.Delete(c);
            unitOfWork.Save();
            return RedirectToAction("Index", "Home");
        }

       
        private Class GetClass(int id)
        {
            var classOptions = new QueryOptions<Class>
            {
                Includes = "Teacher, Day",
                Where = c => c.ClassId == id
            };

          

            return unitOfWork.Classes.Get(classOptions) ?? new Class();
        }
        private void LoadViewBag(string operation)
        {
            ViewBag.Days = unitOfWork.Days.List(new QueryOptions<Day> {
                OrderBy = d => d.DayId
            });
            ViewBag.Teachers = unitOfWork.Teachers.List(new QueryOptions<Teacher> {
                OrderBy = t => t.LastName
            });
            ViewBag.Operation = operation;
        }
    }
}
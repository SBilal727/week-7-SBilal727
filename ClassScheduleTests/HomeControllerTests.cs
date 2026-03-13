using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;
using ClassSchedule.Controllers;
using Microsoft.AspNetCore.Http;

namespace ClassScheduleTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {

            // mocked repositories

            var dayRepo = new Mock<IRepository<Day>>();
            var classRepo = new Mock<IRepository<Class>>();

            // mocked unit of work

            var unitOfWork = new Mock<IClassScheduleUnitOfWork>();

            // mocked HttpContextAccessor

            var httpContextAccessor = new Mock<IHttpContextAccessor>();



            dayRepo.Setup(r => r.List(It.IsAny<QueryOptions<Day>>()))
                   .Returns(new List<Day>());

            classRepo.Setup(r => r.List(It.IsAny<QueryOptions<Class>>()))
                     .Returns(new List<Class>());

            unitOfWork.Setup(u => u.Days).Returns(dayRepo.Object);
            unitOfWork.Setup(u => u.Classes).Returns(classRepo.Object);


            // controller
            var controller = new HomeController(
                unitOfWork.Object,


                  httpContextAccessor.Object);

            
            ViewResult result = controller.Index(0);

            
            Assert.IsType<ViewResult>(result);
        }
    }
}

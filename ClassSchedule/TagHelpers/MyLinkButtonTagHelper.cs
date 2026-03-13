using ClassSchedule.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Xml.Linq;


namespace ClassSchedule.TagHelpers
{
    // 13. Add a class named MyLinkButtonTagHelper that inherits the TagHelper class.
    public class MyLinkButtonTagHelper : TagHelper
    {

        // #15. Update the class to receive a LinkGenerator object
        //      and a ViewContext object via dependency injection.
        private LinkGenerator linkGenerator { get; set; }

        public MyLinkButtonTagHelper(LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
        }


        [ViewContext]
        public ViewContext ViewContext { get; set; }

        // #14. Add public string properties named Action, Controller, and
        //      Id as well as a public Boolean property named IsActive.
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Id { get; set; }
        public bool IsActive { get; set; }

        // #16. In the Process() method, assign the value of the Action property to a string
        //      variable.Or, if the Action property is null, assign the value of the “action”
        //      item of the RouteData.Values collection.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string action = Action ?? ViewContext.RouteData.Values["action"].ToString();

            // # 17. Repeat the previous step for the Controller property.

            string controller = Controller ?? ViewContext.RouteData.Values["controller"].ToString();

            // #18. Create an anonymous object for the id route segment, and assign it to a
            //      variable.Or, if the Id property is null, assign a null value to the variable.

            var idRoute = Id != null ? new { id = Id } : null;

            // #19. Use the action, controller, and id variables with the
            //      LinkGenerator object to create a URL, and assign this URL to a string variable.

            string url = linkGenerator.GetPathByAction(
                    ViewContext.HttpContext, action, controller, idRoute );

            // #20. Assign Bootstrap CSS classes to a string variable. If the value of the IsActive
            //      property is true, assign the btn and btn - dark classes.Otherwise, assign the btn
            //      and btn - outline - dark classes.

            string btnClasses = IsActive ? "btn btn-dark" : "btn btn-outline-dark";

            // #21. Use the variables for the URL and the CSS classes with the BuildLink()
            //      extension method to transform a non-standard < my - link - button > element to a
            //      standard<a> element.

            output.BuildLink(url, btnClasses);

        }




    }
}

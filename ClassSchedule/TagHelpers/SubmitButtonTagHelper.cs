using ClassSchedule.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ClassSchedule.TagHelpers
{

    // #10. Add an HtmlTargetElement attribute that applies this tag helper to all submit buttons.
    
        [HtmlTargetElement("button", Attributes = "type=submit")]

    // #9. Add a class named SubmitButtonTagHelper that inherits the TagHelper class.
    public class SubmitButtonTagHelper : TagHelper
    {
        // 11. Override the Process() method and add code that uses the AppendCssClass()
        //     extension method to apply the Bootstrap btn and btn-dark classes.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.AppendCssClass("btn btn-dark");
        }
    }
}

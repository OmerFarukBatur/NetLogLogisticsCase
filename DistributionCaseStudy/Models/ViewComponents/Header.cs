using Microsoft.AspNetCore.Mvc;

namespace DistributionCaseStudy.Models.ViewComponents
{
    public class Header : ViewComponent
    {
        public Header()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

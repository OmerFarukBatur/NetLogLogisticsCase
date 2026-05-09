using Microsoft.AspNetCore.Mvc;

namespace DistributionCaseStudy.Models.ViewComponents
{
    public class Footer : ViewComponent
    {
        public Footer()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

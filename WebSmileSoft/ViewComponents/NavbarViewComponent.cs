using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(viewName: "_Navbar");
        }
    }
}

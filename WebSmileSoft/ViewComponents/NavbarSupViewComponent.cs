using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.ViewComponents
{
    public class NavbarSupViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("_NavbarSup");
        }
    }
}

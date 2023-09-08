using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("_Sidebar");
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(viewName: "_Footer");
        }
    }
}

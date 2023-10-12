using Microsoft.AspNetCore.Mvc;
using WebSmileSoft.Interfaces;

namespace WebSmileSoft.ViewComponents
{
    public class CommonDataViewComponent : ViewComponent
    {
        private readonly ISettings _settings;

        public CommonDataViewComponent(ISettings settings)
        {
            _settings = settings;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.urlEndPoint = _settings.urlEndPoint;
            return View();
        }
    }
}

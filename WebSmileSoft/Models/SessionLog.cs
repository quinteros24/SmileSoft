using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.Models
{
    public class SessionLog : Controller
    {
        // GET: SessionLog
        public ActionResult Index()
        {
            return View();
        }

        // GET: SessionLog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SessionLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SessionLog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SessionLog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SessionLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SessionLog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SessionLog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

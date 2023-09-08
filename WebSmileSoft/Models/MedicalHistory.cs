using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.Models
{
    public class MedicalHistory : Controller
    {
        // GET: MedicalHistory
        public ActionResult Index()
        {
            return View();
        }

        // GET: MedicalHistory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MedicalHistory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalHistory/Create
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

        // GET: MedicalHistory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MedicalHistory/Edit/5
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

        // GET: MedicalHistory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MedicalHistory/Delete/5
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

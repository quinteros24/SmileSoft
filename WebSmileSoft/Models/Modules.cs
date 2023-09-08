﻿using Microsoft.AspNetCore.Mvc;

namespace WebSmileSoft.Models
{
    public class Modules : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }

        // GET: Modules/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
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

        // GET: Modules/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Modules/Edit/5
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

        // GET: Modules/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Modules/Delete/5
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

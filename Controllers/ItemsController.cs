using Microsoft.AspNetCore.Mvc;
using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingList.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ItemsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.Items.ToList();

            if (items == null)
            {
                return View("Not Found");
            }

            return View(items);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }
        
        // POST
        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Details,Amount")] Item item)
        {
            if(ModelState.IsValid)
            {
                _db.Add(item);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var item = _db.Items.FirstOrDefault(i => i.Id == id);

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name,Details,Amount")] Item item)
        {
            if(id != item.Id )
            {
                return NotFound();
            }

            _db.Update(item);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: Items/Delete/id
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _db.Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/id
        [HttpPost]
        public IActionResult Delete(int id, bool notUsed)
        {
            var item = _db.Items.FirstOrDefault(i => i.Id == id);
            _db.Items.Remove(item);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Search(string query)
        {
            if (query == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var item = _db.Items.Where(i => i.Name.ToLower().Contains(query.ToLower())).ToList();



            if (item == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }
    }
}

using EFOrderTask.Data;
using EFOrderTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFOrderTask.Controllers
{
    public class UnitController : Controller
    {
        private readonly AppDbContext _db;

        public UnitController(AppDbContext db)
        {
            _db=db;
        }
        public IActionResult Index(string sortOrder, string searchString)
        {
            var list = _db.Units.ToList();

            ViewData["UnitTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           
            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(i => i.Unit_Name.Contains(searchString) ).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(i => i.Unit_Name).ToList();
                    break;
               
                default:
                    list = list.OrderBy(i => i.Unit_Name).ToList();
                    break;
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Unit unit)
        {
            _db.Units.Add(unit);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var edit = _db.Units.Where(x => x.Unit_Id ==id).FirstOrDefault();
            return View(edit);
        }

        [HttpPost]
        public IActionResult Edit(Unit unit)
        {
            //var update = _db.Units.Where(x => x.Unit_Id == unit.Unit_Id ).FirstOrDefault();
            //if (unit != null)
            //{
            //    unit.Unit_Name = update.Unit_Name;
            //    _db.Units.Update(update);
            //    _db.SaveChanges();
            //    return View(update);
            //}
            //return RedirectToAction("Index");
            
            _db.Units.Update(unit);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var del = _db.Units.Find(id);
            _db.Units.Remove(del);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

using EFOrderTask.Data;
using EFOrderTask.Models;
using EFOrderTask.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFOrderTask.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public AddItemDetailsViewModel ItemDetailsViewModel { get; set; }
        public ItemController(AppDbContext db)
        {
            _db = db;
            ItemDetailsViewModel = new AddItemDetailsViewModel()
            {
                Items = _db.Items.ToList(),
                Units = _db.Units.ToList(),
            };
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "price_Asc";
            //ViewData["PriceSortParm"] = String.IsNullOrEmpty(sortOrder) ? "price_Asc" : "";
            ViewData["UnitTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "UnitType_desc" : "UnitType_Asc";
            ViewData["QuantitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "Quantity_desc" : "";
            ViewData["CurrentFilter"] = searchString;


            var items = await _db.Items.Include(x => x.ItemUnits)
                            .ThenInclude(u => u.Unit)
                            .Where(x => x.ItemUnits.Count > 0).ToListAsync();

            List<ItemViewModel> sd = new List<ItemViewModel>();
            foreach (var x in items)
            {
                foreach (var i in x.ItemUnits.ToList())
                {
                    var li = new ItemViewModel
                    {
                        ItemId = i.Item.Item_Id,
                        ItemName = i.Item.Item_Name,
                        UnitId = i.UnitId_FK,
                        // UnitType = i.Unit.Unit_Name ,
                        UnitType = i.Unit.Unit_Name,
                        Price = i.Price

                    };
                    sd.Add(li);
                }

            }
            if (!String.IsNullOrEmpty(searchString))
            {
                sd = sd.Where(i => i.ItemName.Contains(searchString) || i.UnitType.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sd = sd.OrderByDescending(i => i.ItemName).ToList();
                    break;
                case "price_desc":
                    sd = sd.OrderByDescending(i => i.Price).ToList();
                    break;
                case "price_Asc":
                    sd = sd.OrderBy(i => i.Price).ToList();
                    break;
                case "UnitType_desc":
                    sd = sd.OrderByDescending(i => i.UnitType).ToList();
                    break;
                case "UnitType_Asc":
                    sd = sd.OrderBy(i => i.UnitType).ToList();
                    break;

                default:
                    sd = sd.OrderBy(i => i.ItemName).ToList();
                    break;
            }

            
            return View(sd);
        }

        [HttpGet]
        public IActionResult RegisteredItems(string sortOrder, string searchString)
        {
            var items = _db.Items.ToList();
            ViewData["UnitTypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "UnitType_desc" : "";
            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Item_Name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "UnitType_desc":
                    items = items.OrderByDescending(i => i.Item_Name).ToList();
                    break;
                
                default:
                    items = items.OrderBy(i => i.Item_Name).ToList();
                    break;
            }
            return View(items);
        }

        [HttpGet]
        public IActionResult CreateItem()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateItem(Item item)
        {

            _db.Items.Add(item);
            _db.SaveChanges();
            return RedirectToAction("RegisteredItems");

        }


        // AddItemDetailsViewModel
        [HttpGet]
        public IActionResult AddItemDetails()
        {
            return View(ItemDetailsViewModel);
        }

        [HttpPost]
        public IActionResult AddItemDetails(AddItemDetailsViewModel addItemVM)
        {

            ItemUnit itemUnit = new ItemUnit()
            {
                ItemId_FK = addItemVM.ItemId,
              
                UnitId_FK = addItemVM.UnitId,
                Price = addItemVM.UnitPrice
            };
            _db.UnitItems.Add(itemUnit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult ItemDetails(int id)
        {
            var details = _db.Items.Include(x => x.ItemUnits).ThenInclude(x => x.Unit).Where(x => x.Item_Id == id).FirstOrDefault();
            var detailsVM = new ItemViewModel()
            {

                ItemId = details.Item_Id,
                ItemName = details.Item_Name,
                //Price = details.ItemUnits.Select(x => x.Price).ToList();
                
                //UnitType = details.ItemUnits.Select(x=>x.Unit.Unit_Name),
                //UnitType = details.ItemUnits.Select(x=>x.Unit.Unit_Name).ToList(),
                //UnitId = details.ItemUnits.Select(x=> x.Unit.Unit_Id == id).ToList(),
                //UnitId = details.ItemUnits.Where(x => x.ItemId_FK == id),
                //Units = details.ItemUnits.Select(x => x.UnitId_FK == id).ToList(),
            };
            return View(detailsVM);
        }

        public IActionResult Delete(int id)
        {
            var del = _db.Items.Find(id);
            _db.Items.Remove(del);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // git
        public IActionResult DeleteItem(int id)
        {
            var del = _db.UnitItems.Find(id);
            _db.UnitItems.Remove(del);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
          //git
    }
}



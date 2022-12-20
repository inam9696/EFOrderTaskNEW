using EFOrderTask.Data;
using EFOrderTask.Models;
using EFOrderTask.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFOrderTask.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _db;
        private List<Item> items;

        [BindProperty]
        public PlaceOrderViewModel placeOrderVM { get; set; }
        public OrderController(AppDbContext db)
        {
            _db=db;
        }
        public IActionResult Index1()
        {
            var list = _db.Orders.ToList();
            return View(list);
        }


        [HttpGet]
        public IActionResult OrderedItem(int id)
        {
            var items = _db.OrderedItems.Include(i=>i.Item).Include(u=>u.Unit).Where(oi=>oi.OrderId_FK == id).ToList();
            return View(items);
        }

        public IActionResult DeleteSingleOrderItem(int id)
        {

            var item = _db.OrderedItems.Where(oi => oi.Order_Id == id).FirstOrDefault();
            var order = _db.Orders.Where(o => o.Order_Id == item.OrderId_FK).FirstOrDefault();
            var totalPrice = order.Total_Price;
            totalPrice = totalPrice - item.Sub_Total;
            order.Total_Price = totalPrice;
            _db.Orders.Update(order);
            _db.OrderedItems.Remove(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditItems(int id)
        {

            OrderItem item = _db.OrderedItems.Where(oi => oi.Order_Id == id).FirstOrDefault();
            return View(item);
        }

        [HttpPost]
        public IActionResult EditItems(OrderItem model)
        {

            var order = _db.Orders.Where(o => o.Order_Id == model.OrderId_FK).FirstOrDefault();
            _db.OrderedItems.Update(model);
            _db.SaveChanges();
            List<OrderItem> oi = _db.OrderedItems.Where(o => o.OrderId_FK == model.OrderId_FK && o.ItemId_Fk == model.ItemId_Fk && o.UnitId_Fk == model.UnitId_Fk).ToList();
            decimal? totalPrice = 0;
            if (oi != null)
            {
                foreach(var i in oi)
                {
                    totalPrice = (totalPrice + i.Sub_Total);
                }
               
            }
            order.Total_Price = totalPrice;
            _db.Orders.Update(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
                        //Quantity =items.ItemUnits.Select(x=>x.Quatity).ToList(),
                        //Quantity =items.Select(x=>x.ItemUnits).Where(x=)
                        UnitType = i.Unit.Unit_Name,
                        Price = i.Price

                    };
                    sd.Add(li);
                }

            }
            if (!String.IsNullOrEmpty(searchString))
            {
                sd = sd.Where(i => i.ItemName.Contains(searchString) ||
                                   i.UnitType.Contains(searchString)).ToList();
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
        public async Task<IActionResult> PlaceOrder(string? customerName, string? customerGuid, int oId, Decimal TotalPrice = 0)
        {
            var items = (from item in _db.Items

                         join ItemUnit in _db.UnitItems on item.Item_Id equals ItemUnit.ItemId_FK
                         select new Item()
                         {
                             Item_Id = item.Item_Id,
                             Item_Name = item.Item_Name,
                         });

            var uniqueItems = items.Distinct().ToList();

            if (customerName != null)
            {
                placeOrderVM = new PlaceOrderViewModel()
                {
                    Items = uniqueItems,
                    Customer_Name = customerName,
                    CustomerGuidKey = customerGuid,
                    Order_Id = oId,
                    Total_Price = TotalPrice
                    
                };

            }
            return View(placeOrderVM);
        }


        [HttpPost]
        public IActionResult PlaceOrder(PlaceOrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var Order_Id = orderViewModel.Order_Id;
                
                var totalprice = orderViewModel.Total_Price;
                var pricePerUnit = from UnitItem in _db.UnitItems
                                   where UnitItem.ItemId_FK == orderViewModel.Item_Id && UnitItem.UnitId_FK == orderViewModel.Unit_Id
                                   select new
                                   {
                                       PricePerUnit = UnitItem.Price,
                                   };
                decimal priceOfItem = 0;
                foreach (var p in pricePerUnit)
                {
                    priceOfItem = p.PricePerUnit;
                };
                var sub_total = priceOfItem * Convert.ToDecimal(orderViewModel.Quantity);
                OrderItem orderedItem = new OrderItem()
                {
                    
                    ItemId_Fk = orderViewModel.Item_Id,
                    OrderId_FK = orderViewModel.Order_Id,
                    UnitId_Fk = orderViewModel.Unit_Id,
                    CustomerGuidKey = orderViewModel.CustomerGuidKey,
                    Customer_Name = orderViewModel.Customer_Name,
                    Quantity = orderViewModel.Quantity,
                    Sub_Total = sub_total
                };

                _db.OrderedItems.Add(orderedItem);
                _db.SaveChanges();


                Order order = _db.Orders.Where(o => o.Order_Id == Order_Id).FirstOrDefault();
                var previousPrice = order.Total_Price;
                previousPrice += (sub_total + totalprice);
                order.Total_Price = previousPrice;

                _db.Orders.Update(order);
                _db.SaveChanges();


            }
            return RedirectToAction("Index");
        }
        //
        //
        public IActionResult AddItem(PlaceOrderViewModel placeOrderVM)
        {
            if (ModelState.IsValid)
            {
                var C_Name = placeOrderVM.Customer_Name.ToString();
                var C_Guid = placeOrderVM.CustomerGuidKey.ToString();
                var orderId = placeOrderVM.Order_Id;

                var unitPrice = from UnitItem in _db.UnitItems
                                   where UnitItem.ItemId_FK == placeOrderVM.Item_Id && UnitItem.UnitId_FK == placeOrderVM.Unit_Id
                                   select new
                                   {
                                       PricePerUnit = UnitItem.Price,
                                   };
                decimal priceOfItem = 0;
                foreach (var p in unitPrice)
                {
                    priceOfItem = p.PricePerUnit;
                };
                var sub_total = priceOfItem * Convert.ToDecimal(placeOrderVM.Quantity);
                OrderItem orderedItem = new OrderItem()
                {
                    ItemId_Fk = placeOrderVM.Item_Id,
                    OrderId_FK = placeOrderVM.Order_Id,
                    UnitId_Fk = placeOrderVM.Unit_Id,
                    CustomerGuidKey = placeOrderVM.CustomerGuidKey,
                    Customer_Name = placeOrderVM.Customer_Name,
                    Quantity = placeOrderVM.Quantity,
                    Sub_Total = sub_total
                };
                _db.OrderedItems.Add(orderedItem);
                _db.SaveChanges();

                var oItem = from OrderItem in _db.OrderedItems
                            where OrderItem.OrderId_FK == orderId
                            select new OrderItem
                            {
                                Sub_Total = OrderItem.Sub_Total,
                            };
                Decimal totalPrice = 0;
                foreach (var p in oItem)
                {
                    totalPrice += ((Decimal)p.Sub_Total.Value);
                }

                return RedirectToAction("placeOrder", new { customerName = C_Name, customerGuid = C_Guid.ToString(), oId = orderId, TotalPrice = totalPrice });


            }
            return RedirectToAction("Index");
        }


        //[HttpGet]
        [HttpGet]
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder( Order order)
        {
            var cName = order.Customer_Name.ToString();
            string cGuid = Guid.NewGuid().ToString();
            order.Date = DateTime.UtcNow;
            order.CustomerGuidKey = cGuid;
            order.Total_Price = 0;
            _db.Orders.Add(order);
            _db.SaveChanges();
            int orderId = _db.Orders.Where(o => o.CustomerGuidKey == cGuid).Select(o => o.Order_Id).FirstOrDefault();
            //return redire
            //return RedirectToAction("Index");
            return RedirectToAction("placeOrder", new { customerName = cName, customerGuid = cGuid.ToString(), oId = orderId });
        }


        [HttpGet]
        public IActionResult getUnits(int id, string CName, string CGuid, string oid)
        {

            var items = (from item in _db.Items

                         join ItemUnit in _db.UnitItems on item.Item_Id equals ItemUnit.ItemId_FK
                         select new Item()
                         {
                             Item_Id = item.Item_Id,
                             Item_Name = item.Item_Name,

                         });
            var uniqueItems = items.Distinct().ToList();

            var units = from unit in _db.Units
                        from UnitItem in _db.UnitItems
                        where unit.Unit_Id == UnitItem.UnitId_FK && UnitItem.ItemId_FK == id
                        select new Unit()
                        {
                            Unit_Id = unit.Unit_Id,
                            Unit_Name = unit.Unit_Name
                        };


            UnitsViewModel getUnitsVM = new UnitsViewModel()
            {
                Units = units.ToList(),


            };
            return PartialView("_getUnitsPV", getUnitsVM);
        }


            [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }




        //[HttpGet]
        //public IActionResult OrderList(OrderViewModel model)
        //{
        //    var orderList = _db.Items.Include(x => x.OrderedItems)
        //                                .Include(x => x.UnitItems)
        //                                .ThenInclude(x=>x.Unit)
        //                                .ThenInclude(x => x.Unit_Name)
        //                                .Include(x => x.Item_Id)
        //                                .Include(x => x.Item_Name)
        //                                //.ThenInclude(x=>x.qu)
        //                                .Include(x => x.Price)/*.ToList()*/;
        //                              //.Include(x=>x)
        //                              //.ThenInclude(u => u.Item_Name)
        //                              //.Include(y => y.uni)
        //                              //.ThenInclude(u => u.UnitId_FK)
        //                              //.ThenInclude(y => y.item)
        //                              //.Include(x => x.Quantity)
        //                              //.Include(x => x.Order)
        //                              //.ThenInclude(x => x.TotalPrice)
        //                             // .Include(x => x.Sub_Total)
        //                              //.Include(x => x.Customer_Name).ToList();
        //    var order = new OrderViewModel()
        //    {

        //        ItemId = model.ItemId,
        //        UnitId = model.UnitId,

        //        // = model.Item_Name,
        //        Quantity = model.Quantity

        //        //UnitType = model.Unit_Name
        //    };
        //    return View(orderList);
        //    //return RedirectToAction("OrderList");
        // }

        public class Test
        {
            public int? id { get; set; }
            public int? oid { get; set; }
            public string? CName { get; set; }
            public string? CGuid { get; set; }

        }

    }
}

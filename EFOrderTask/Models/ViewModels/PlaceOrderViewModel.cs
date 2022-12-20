using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFOrderTask.Models.ViewModels
{
    public class PlaceOrderViewModel
    {
        public int Order_Id { get; set; }
        public int Item_Id { get; set; }
        public int Unit_Id { get; set; }
        public ICollection<Item>? Items { get; set; }
        public ICollection<Unit>? Units { get; set; }
        public ICollection<ItemUnit>? ItemUnits { get; set; }
        public string? Customer_Name { get; set; }
        public int Quantity { get; set; }
        public string? CustomerGuidKey { get; set; }
        public Decimal Total_Price { get; set; }
        

        //List Items
        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Item> Items)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem Sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listItems.Add(Sli);
            foreach (Item item in Items)
            {
                Sli = new SelectListItem
                {
                    Text = item.Item_Name,
                    Value = item.Item_Id.ToString(),
                };
                listItems.Add(Sli);

            }
            return listItems;
        }

        

        public IEnumerable<SelectListItem>? CSelectListUnit(IEnumerable<Unit>? Items)
        {
            List<SelectListItem> listUnits = new List<SelectListItem>();
            SelectListItem Sli = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listUnits.Add(Sli);
            if (Items != null)
            {


                foreach (Unit item in Items)
                {
                    Sli = new SelectListItem
                    {
                        Text = item.Unit_Name,
                        Value = item.Unit_Id.ToString()
                    };
                    listUnits.Add(Sli);

                }
                return listUnits;
            }
            return null;
        }

    }
}

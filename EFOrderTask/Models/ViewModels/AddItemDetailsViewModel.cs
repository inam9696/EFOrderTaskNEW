using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFOrderTask.Models.ViewModels
{
    public class AddItemDetailsViewModel
    {
        public Decimal UnitPrice { get; set; }
        //public int Quatity { get; set; }
        public int ItemId { get; set; }
        public ICollection<Item>? Items { get; set; }
        public int UnitId { get; set; }
        //public string  { get; set; }
        public ICollection<Unit>? Units { get; set; }

        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Item> Items)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem selectListItem = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listItems.Add(selectListItem);
            foreach (Item item in Items)
            {
                selectListItem = new SelectListItem
                {
                    Text = item.Item_Name,
                    Value = item.Item_Id.ToString(),
                };
                listItems.Add(selectListItem);

            }
            return listItems;
        }


        public IEnumerable<SelectListItem> CSelectListUnits(IEnumerable<Unit> units)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem selectListItem = new SelectListItem
            {
                Text = "---Select---",
                Value = "0"
            };
            listItems.Add(selectListItem);
            foreach (Unit unit in Units)
            {
                selectListItem = new SelectListItem
                {
                    Text = unit.Unit_Name,
                    Value = unit.Unit_Id.ToString(),
                };
                listItems.Add(selectListItem);

            }
            return listItems;
        }

    }
}

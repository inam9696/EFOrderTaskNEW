using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFOrderTask.Models.ViewModels
{
    public class UnitsViewModel
    {
        public int Unit_Id { get; set; }
        public ICollection<Unit>? Units { get; set; }
        public int? Quantity { get; set; }




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

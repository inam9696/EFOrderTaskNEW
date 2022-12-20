using System.ComponentModel.DataAnnotations;

namespace EFOrderTask.Models.ViewModels
{
    public class ItemViewModel
    {

        public IList<Item> Items { get; set; }
        public IList<Unit> Units { get; set; }
        public int? ItemId { get; set; }
        public int? UnitId { get; set; }
        public string UnitType { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
     
    }
}

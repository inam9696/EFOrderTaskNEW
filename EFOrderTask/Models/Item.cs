namespace EFOrderTask.Models
{
    public class Item
    {
        public int Item_Id { get; set; }
        public string Item_Name { get; set; }

        //public decimal? Item_Price { get; set; }
       
        public virtual ICollection<ItemUnit>? ItemUnits { get; set; } = new HashSet<ItemUnit>();

        public virtual ICollection<OrderItem>? OrderedItems { get; set; } = new List<OrderItem>();
    }
}

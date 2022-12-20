using System.ComponentModel.DataAnnotations;

namespace EFOrderTask.Models
{
    public class OrderItem
    {
        [Key]
        public int Order_Id { get; set; }
        public int? ItemId_Fk { get; set; }
        public int? OrderId_FK { get; set; }
        public int? UnitId_Fk { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
        public virtual Unit Unit { get; set; }
        

        public String Customer_Name { get; set; }
        public int? Quantity { get; set; }
        public string? CustomerGuidKey { get; set; }
        public decimal? Sub_Total { get; set; }
    }
}

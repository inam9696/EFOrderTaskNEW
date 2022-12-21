using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EFOrderTask.Models
{
    public class Order
    {
        public int Order_Id { get; set; }
        public DateTime? Date { get; set; }

        public decimal? Total_Price { get; set; }
        public string Customer_Name { get; set; }
        //public string item { get; set; }
        public string? CustomerGuidKey { get; set; }
        public virtual ICollection<OrderItem>? OrderedItems { get; set; }
    }
}

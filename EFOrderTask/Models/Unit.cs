using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EFOrderTask.Models
{
    public class Unit
    {
        public int Unit_Id { get; set; }
        public string Unit_Name { get; set; }

        public virtual ICollection<ItemUnit>? ItemUnits { get; set; }
        public virtual ICollection<OrderItem>? OrderedItems { get; set; }
    }
}

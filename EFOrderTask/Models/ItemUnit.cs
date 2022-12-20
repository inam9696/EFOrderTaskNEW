namespace EFOrderTask.Models
{
    public class ItemUnit
    {
        public virtual Item Item { get; set; }
        public int ItemId_FK { get; set; }

        public virtual Unit Unit { get; set; }
        public int UnitId_FK { get; set; }

        public Decimal Price { get; set; }
        public int? Quatity { get; set; }
    }
}

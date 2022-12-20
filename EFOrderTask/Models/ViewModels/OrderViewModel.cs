namespace EFOrderTask.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }
        public string Item_Name { get; set; }
        public string Unit_Name { get; set; }

        public String? Customer_Name { get; set; }
        public string? CustomerGuidKey { get; set; }
        public int? Quantity { get; set; }
        public decimal? Sub_Total { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}

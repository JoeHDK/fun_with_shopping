namespace WebshopApi.Models;
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
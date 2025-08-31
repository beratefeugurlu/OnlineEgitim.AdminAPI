namespace OnlineEgitim.AdminAPI.Models
{
    public class Order
    {
        public int Id { get; set; }

        // Siparişi veren kullanıcı
        public int UserId { get; set; }
        public User User { get; set; }

        // Sipariş tarihi
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // Sipariş içindeki kurslar
        public List<OrderItem> Items { get; set; } = new();
    }
}

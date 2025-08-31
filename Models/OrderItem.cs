namespace OnlineEgitim.AdminAPI.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Hangi siparişe bağlı
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Hangi kurs
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Kaç adet
        public int Quantity { get; set; }
    }
}


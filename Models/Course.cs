namespace OnlineEgitim.AdminAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructor { get; set; }
        public decimal Price { get; set; }

        // Açıklama eklendi
        public string Description { get; set; }

        // OrderItem ilişkisi
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

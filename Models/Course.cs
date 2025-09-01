namespace OnlineEgitim.AdminAPI.Models
{
    public class Course
    {
        public int Id { get; set; }

        // Kurs başlığı
        public string Title { get; set; } = string.Empty;

        // Eğitmen bilgisi
        public string Instructor { get; set; } = string.Empty;

        // Fiyat
        public decimal Price { get; set; }

        // Açıklama
        public string Description { get; set; } = string.Empty;

        // ✅ Admin onayı (ekledik)
        public bool IsApproved { get; set; } = false;

        // OrderItem ilişkisi
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

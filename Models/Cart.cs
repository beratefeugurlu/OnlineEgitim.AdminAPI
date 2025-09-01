namespace OnlineEgitim.AdminAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // Kullanıcı ilişkisi
        public int UserId { get; set; }
        public User User { get; set; }

        // Course ilişkisi
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Sepet için adet
        public int Quantity { get; set; }
    }
}

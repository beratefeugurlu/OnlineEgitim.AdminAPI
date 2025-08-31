namespace OnlineEgitim.AdminAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // Hangi kullanıcıya ait
        public int UserId { get; set; }
        public User User { get; set; }

        // Hangi kurs eklendi
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Kaç adet eklendi (opsiyonel, default 1 olabilir)
        public int Quantity { get; set; } = 1;
    }
}

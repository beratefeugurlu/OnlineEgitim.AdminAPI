namespace OnlineEgitim.AdminAPI.Models
{
    public class User
    {
        public int Id { get; set; } // Birincil anahtar
        public string Name { get; set; } = string.Empty; // Kullanıcı adı veya tam ad
        public string Email { get; set; } = string.Empty; // Giriş için email
        public string PasswordHash { get; set; } = string.Empty; // Şifre (hashlenecek)
        public string Role { get; set; } = "Student"; // Varsayılan rol: Student
    }
}

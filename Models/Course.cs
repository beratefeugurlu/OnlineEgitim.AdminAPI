namespace OnlineEgitim.AdminAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; } // opsiyonel alan ,string? null olabilir demektir!!
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

        
    }
}

namespace Browlines_API.Model
{
    public class Customer_Model
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? Email { get; set; }

        public string? ContactNo { get; set; }

        public string? Address { get; set; }

        public string? Notes { get; set; }

        public string? Complaints { get; set; }

        public string? Allergies { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual User_Model? CreatedByNavigation { get; set; }
        public virtual User_Model? UpdatedByNavigation { get; set; }
        public virtual ICollection<Transaction_Model> CustomerRecords { get; set; } = new List<Transaction_Model>();
    }
}

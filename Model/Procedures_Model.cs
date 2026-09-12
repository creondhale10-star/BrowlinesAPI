namespace Browlines_API.Model
{
    public class Procedures_Model
    {
        public int Id { get; set; }
        public String? Service { get; set; }
        public String? Procedure { get; set; }
        public decimal? Amount { get; set; }
        public int? Duration { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual User_Model? CreatedByNavigation { get; set; }     
        public virtual User_Model? UpdatedByNavigation { get; set; }

    }
}

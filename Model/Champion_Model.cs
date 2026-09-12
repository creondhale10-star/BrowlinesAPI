namespace Browlines_API.Model
{
    public class Champion_Model
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public TimeOnly? AvailableFrom { get; set; }
        public TimeOnly? AvailableTo { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdateBy { get; set; }
        public virtual User_Model? CreatedByNavigation { get; set; }
        public virtual User_Model? UpdateByNavigation { get; set; }
        public virtual ICollection<Transaction_Procedures_Model> TransactionProcedureChampionRecord { get; set; } = new List<Transaction_Procedures_Model>();
    }
}

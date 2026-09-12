using System.ComponentModel.DataAnnotations.Schema;

namespace Browlines_API.Model
{
    public class Transaction_Model
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public String? Status { get; set; }
        public String? PaymentStatus { get; set; }
        [NotMapped]
        public String? Customers { get; set; }
        public Decimal? TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Customer_Model? CustomerNavigation { get; set; }
        public virtual ICollection<Transaction_Procedures_Model> TransactionProcedureRecords { get; set; } = new List<Transaction_Procedures_Model>();


        
    }
}

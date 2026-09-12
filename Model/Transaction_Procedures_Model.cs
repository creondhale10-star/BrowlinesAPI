using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Browlines_API.Model
{
    public class Transaction_Procedures_Model
    {
        public int? Id { get; set; }
        public int? TransactionId { get; set; }
        public int? ChampionId { get; set; }
        [NotMapped]
        public String? Champion { get; set; }
        public String? Service { get; set; }
        public String? Procedure { get; set; }
        public decimal? Amount { get; set; }
        public DateOnly? ScheduleDate { get; set; }
        public TimeOnly? ScheduleTime { get; set; }
        public String? Status { get; set; }
        
        [JsonIgnore]
        public virtual Transaction_Model? TransactionNavigation { get; set; }
        public virtual Champion_Model? ChampionNavigation { get; set; }
    }
}

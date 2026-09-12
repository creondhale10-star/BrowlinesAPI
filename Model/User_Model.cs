using System.Text.Json.Serialization;

namespace Browlines_API.Model
{
    public class User_Model
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Pass { get; set; }

        public string? Name { get; set; }

        public DateOnly? Birthdate { get; set; }

        public string? ContactNo { get; set; }

        public string? Address { get; set; }
        public string? Role { get; set; }

        public string? About { get; set; }

        public byte[]? Img { get; set; }
        
        public virtual ICollection<Procedures_Model> ProcedureCreatedByRecords { get; set; } = new List<Procedures_Model>();
        public virtual ICollection<Procedures_Model> ProcedureUpdatedByRecords { get; set; } = new List<Procedures_Model>();
        public virtual ICollection<Customer_Model> CustomerCreatedByRecords { get; set; } = new List<Customer_Model>();
        public virtual ICollection<Customer_Model> CustomerUpdatedByRecords { get; set; } = new List<Customer_Model>();
        public virtual ICollection<Champion_Model> ChampionCreatedByNavigations { get; set; } = new List<Champion_Model>();
        public virtual ICollection<Champion_Model> ChampionUpdateByNavigations { get; set; } = new List<Champion_Model>();

    }
}

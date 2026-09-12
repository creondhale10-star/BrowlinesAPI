using Browlines_API.Model;

namespace Browlines_API.Interface
{
    public interface IProcedures_Services
    {
        Task<List<Procedures_Model>> GetAllAsync();
        Task<Procedures_Model?> GetAllByIdAsync(int vId);
        Task<Procedures_Model> CreateAsync(Procedures_Model procedure);
        Task<bool> UpdateAsync(Procedures_Model procedure);
        Task<bool> DeleteAsync(int vId);
    }
}

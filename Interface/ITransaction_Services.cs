using Browlines_API.Model;

namespace Browlines_API.Interface
{
    public interface ITransaction_Services
    {
        Task<List<Transaction_Model>> GetAllAsync();
        Task<Transaction_Model?> GetAllByIdAsync(int vId);
        Task<Transaction_Model> CreateAsync(Transaction_Model procedure);
        Task<bool> UpdateAsync(Transaction_Model procedure);
        Task<bool> DeleteAsync(int vId);
    }
}

using Browlines_API.Model;

namespace Browlines_API.Interface
{
    public interface ICustomer_Services
    {
        Task<List<Customer_Model>> GetAllAsync();
        Task<Customer_Model?> GetAllByIdAsync(int vId);
        Task<Customer_Model> CreateAsync(Customer_Model procedure);
        Task<bool> UpdateAsync(Customer_Model procedure);
        Task<bool> DeleteAsync(int vId);
    }
}

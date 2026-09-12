using Browlines_API.Model;

namespace Browlines_API.Interface
{
    public interface IChampions_Services
    {
        Task<List<Champion_Model>> GetAllAsync();
        Task<Champion_Model?> GetAllByIdAsync(int vId);
        Task<Champion_Model> CreateAsync(Champion_Model procedure);
        Task<bool> UpdateAsync(Champion_Model procedure);
        Task<bool> DeleteAsync(int vId);
    }
}

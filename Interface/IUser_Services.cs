using Browlines_API.Model;

namespace Browlines_API.Interface
{
    public interface IUser_Services
    {
        Task<List<User_Model>> GetAllAsync();
        Task<User_Model?> GetAllByIdAsync(int vId);
        Task<User_Model> CreateAsync(User_Model procedure);
        Task<bool> UpdateAsync(User_Model procedure);
        Task<bool> ChangePass(User_Model procedure);
        Task<bool> DeleteAsync(int vId);
    }
}

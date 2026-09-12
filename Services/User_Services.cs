using Browlines_API.Data;
using Browlines_API.Interface;
using Browlines_API.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Browlines_API.Services
{
    public class User_Services : IUser_Services
    {
        private readonly BrowlinesDBContext _context;

        public User_Services(BrowlinesDBContext context)
        {
            _context = context;
        }

        public async Task<List<User_Model>> GetAllAsync()
        {
            return await _context.Users.Select(x=> new User_Model
            {
                Id = x.Id,
                Username = x.Username,
                Pass = x.Pass,
                Name = x.Name,
                Birthdate = x.Birthdate,
                ContactNo = x.ContactNo,
                Address = x.Address,
                Role = x.Role,
                About = x.About,
                Img = x.Img,
            }).ToListAsync();
        }

        public async Task<User_Model?> GetAllByIdAsync(int vId)
        {
            return await _context.Users.Select(x => new User_Model
            {
                Id = x.Id,
                Username = x.Username,
                Pass = x.Pass,
                Name = x.Name,
                Birthdate = x.Birthdate,
                ContactNo = x.ContactNo,
                Address = x.Address,
                About = x.About,
                Role = x.Role,
                Img = x.Img,
            }).FirstOrDefaultAsync(x => x.Id == vId);  
        }

        public async Task<User_Model> CreateAsync(User_Model model)
        {
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(User_Model model)
        {
            var existingRecord = await _context.Users.FindAsync(model.Id);
            if (existingRecord == null) return false;

            // Update properties
            existingRecord.Username = model.Username;
            existingRecord.Name = model.Name;
            existingRecord.Birthdate = model.Birthdate;
            existingRecord.ContactNo = model.ContactNo;
            existingRecord.Address = model.Address;
            existingRecord.About = model.About;
            existingRecord.Img = model.Img;
            existingRecord.Role = model.Role;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePass(User_Model model)
        {
            var existingRecord = await _context.Users.FindAsync(model.Id);
            if (existingRecord == null) return false;

            // Update properties
            existingRecord.Pass = model.Pass;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _context.Users.FindAsync(id);

            // Kapag walang nahanap na record
            if (existingRecord == null )  return false;
          
            // Burahin sa database context at i-save ang pagbabago
            _context.Users.Remove(existingRecord);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using Browlines_API.Data;
using Browlines_API.Interface;
using Browlines_API.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Browlines_API.Services
{
    public class Customer_Services : ICustomer_Services
    {
        private readonly BrowlinesDBContext _context;

        public Customer_Services(BrowlinesDBContext context)
        {
            _context = context;
        }

        public async Task<List<Customer_Model>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer_Model?> GetAllByIdAsync(int vId)
        {
            return await _context.Customers.FirstOrDefaultAsync(x=> x.Id == vId);            
        }

        public async Task<Customer_Model> CreateAsync(Customer_Model model)
        {
            await _context.Customers.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(Customer_Model model)
        {
            var existingRecord = await _context.Customers.FindAsync(model.Id);
            if (existingRecord == null) return false;

            // Update properties
            existingRecord.Name = model.Name;
            existingRecord.Birthdate = model.Birthdate;
            existingRecord.ContactNo = model.ContactNo;
            existingRecord.Address = model.Address;
            existingRecord.Email = model.Email;
            existingRecord.Notes = model.Notes;
            existingRecord.Complaints = model.Complaints;
            existingRecord.Allergies = model.Allergies;
            existingRecord.UpdatedBy = 1;
            existingRecord.UpdatedAt=DateTime.Now;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _context.Customers.FindAsync(id);

            // Kapag walang nahanap na record
            if (existingRecord == null )  return false;
          
            // Burahin sa database context at i-save ang pagbabago
            _context.Customers.Remove(existingRecord);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

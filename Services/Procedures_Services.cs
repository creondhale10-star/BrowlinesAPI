using Browlines_API.Data;
using Browlines_API.Interface;
using Browlines_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Browlines_API.Services
{
    public class Procedures_Services : IProcedures_Services
    {
        private readonly BrowlinesDBContext _context;

        public Procedures_Services(BrowlinesDBContext context)
        {
            _context = context;
        }

        public async Task<List<Procedures_Model>> GetAllAsync()
        {
            var result = await _context.Procedures.ToListAsync();
            return result;
        }
        public async Task<Procedures_Model?> GetAllByIdAsync(int vId)
        {
            var result = await _context.Procedures.Where(x => x.Id == vId).FirstOrDefaultAsync();         
            return result;
        }
              
        public async Task<Procedures_Model> CreateAsync(Procedures_Model model)
        {        

            await _context.Procedures.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }
        public async Task<bool> UpdateAsync(Procedures_Model model)
        {
            var existingRecord = await _context.Procedures.FindAsync(model.Id);
            if (existingRecord == null) return false;

            // Update properties
            existingRecord.Service = model.Service;
            existingRecord.Procedure = model.Procedure;
            existingRecord.Amount = model.Amount;
            existingRecord.Duration = model.Duration;
            existingRecord.UpdatedBy = model.UpdatedBy;
            existingRecord.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _context.Procedures.FindAsync(id);

            // Kapag walang nahanap na record
            if (existingRecord == null) return false;
         
            // Burahin sa database context at i-save ang pagbabago
            _context.Procedures.Remove(existingRecord);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

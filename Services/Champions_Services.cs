using Browlines_API.Data;
using Browlines_API.Interface;
using Browlines_API.Model;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Browlines_API.Services
{
    public class Champions_Services : IChampions_Services
    {
        private readonly BrowlinesDBContext _context;

        public Champions_Services(BrowlinesDBContext context)
        {
            _context = context;
        }

        public async Task<List<Champion_Model>> GetAllAsync()
        {
            return await _context.Champions.ToListAsync();
        }

        public async Task<Champion_Model?> GetAllByIdAsync(int vId)
        {
            return await _context.Champions.FirstOrDefaultAsync(x=> x.Id == vId);            
        }

        public async Task<Champion_Model> CreateAsync(Champion_Model model)
        {
            await _context.Champions.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(Champion_Model model)
        {
            var existingRecord = await _context.Champions.FindAsync(model.Id);
            if (existingRecord == null) return false;

            // Update properties
            existingRecord.Name = model.Name;
            existingRecord.Specialization = model.Specialization;
            existingRecord.AvailableFrom = model.AvailableFrom;
            existingRecord.AvailableTo = model.AvailableTo;
            existingRecord.Status = model.Status;
            existingRecord.CreatedBy = model.CreatedBy;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _context.Champions.FindAsync(id);

            // Kapag walang nahanap na record
            if (existingRecord == null )  return false;
          
            // Burahin sa database context at i-save ang pagbabago
            _context.Champions.Remove(existingRecord);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

using Browlines_API.Data;
using Browlines_API.Interface;
using Browlines_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Browlines_API.Services
{
    public class Transaction_Services : ITransaction_Services
    {
        private readonly BrowlinesDBContext _context;

        public Transaction_Services(BrowlinesDBContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction_Model>> GetAllAsync()
        {
          return await _context.Transactions
          .Select(x => new Transaction_Model
          {
              Id = x.Id,
              CustomerId = x.CustomerId,
              TotalAmount = x.TotalAmount,
              Status = x.Status,
              PaymentStatus = x.PaymentStatus,              
              Customers = x.CustomerNavigation.Name !=null ? x.CustomerNavigation.Name  : null,
              CreatedAt = x.CreatedAt,
              TransactionProcedureRecords = x.TransactionProcedureRecords.Select(x=> new Transaction_Procedures_Model
              {
                   Id =x.Id,
                    TransactionId = x.TransactionId, 
                    ChampionId = x.ChampionId,
                    Service = x.Service,
                    Procedure = x.Procedure,
                    Amount = x.Amount,
                    Status = x.Status,
                    ScheduleDate = x.ScheduleDate,
                    ScheduleTime = x.ScheduleTime,
                    Champion = x.ChampionNavigation.Name != null ? x.ChampionNavigation.Name : null

              }).ToList()                  
          })
          .ToListAsync();
        }
        public async Task<Transaction_Model?> GetAllByIdAsync(int vId)
        {

        return await _context.Transactions
          .Select(x => new Transaction_Model
          {
              Id = x.Id,
              CustomerId = x.CustomerId,
              TotalAmount = x.TotalAmount,
              Status = x.Status,
              PaymentStatus = x.PaymentStatus,
              Customers = x.CustomerNavigation.Name != null ? x.CustomerNavigation.Name : null,
              CreatedAt = x.CreatedAt,
              TransactionProcedureRecords = x.TransactionProcedureRecords.Select(x => new Transaction_Procedures_Model
              {
                  Id = x.Id,
                  TransactionId = x.TransactionId,
                  ChampionId = x.ChampionId,
                  Service = x.Service,
                  Procedure = x.Procedure,
                  Status = x.Status,
                  Amount = x.Amount,
                  ScheduleDate = x.ScheduleDate,
                  ScheduleTime = x.ScheduleTime,
                  Champion = x.ChampionNavigation.Name != null ? x.ChampionNavigation.Name : null

              }).ToList()
          })
          .Where(x => x.Id == vId).FirstOrDefaultAsync();        
        }
              
        public async Task<Transaction_Model> CreateAsync(Transaction_Model model)
        {

            var transaction = new Transaction_Model
            {
                CustomerId = model.CustomerId,
                TotalAmount = model.TotalAmount,
                Status = model.Status,
                PaymentStatus = model.PaymentStatus,
                CreatedAt = DateTime.Now
            };

            foreach (var item in model.TransactionProcedureRecords)
            {
                transaction.TransactionProcedureRecords.Add(
                    new Transaction_Procedures_Model
                    {
                        ChampionId = item.ChampionId,
                        Service = item.Service,
                        Procedure = item.Procedure,
                        Status = item.Status,
                        Amount = item.Amount,
                        ScheduleDate = item.ScheduleDate,
                        ScheduleTime = item.ScheduleTime
                    });
            }
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return model;
        }
        public async Task<bool> UpdateAsync(Transaction_Model model)
        {
            var transaction = await _context.Transactions.Include(x => x.TransactionProcedureRecords).FirstOrDefaultAsync(x => x.Id == model.Id); 
            if (transaction == null) return false;

            // Update properties
            transaction.CustomerId = model.CustomerId;
            transaction.TotalAmount = model.TotalAmount;
            transaction.Status = model.Status;
            transaction.PaymentStatus = model.PaymentStatus;

            _context.TransactionProcedures.RemoveRange(transaction.TransactionProcedureRecords);
            foreach (var item in model.TransactionProcedureRecords)
            {
                transaction.TransactionProcedureRecords.Add(
                    new Transaction_Procedures_Model
                    {
                        TransactionId = transaction.Id,
                        ChampionId = item.ChampionId,
                        Service = item.Service,
                        Procedure = item.Procedure,
                        Amount = item.Amount,
                        Status = item.Status,
                        ScheduleDate = item.ScheduleDate,
                        ScheduleTime = item.ScheduleTime
                    });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingRecord = await _context.Transactions.FindAsync(id);

            // Kapag walang nahanap na record
            if (existingRecord == null) return false;
         
            // Burahin sa database context at i-save ang pagbabago
            _context.Transactions.Remove(existingRecord);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}

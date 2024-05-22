using InterviewCrudTask.IServices;
using InterviewCrudTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewCrudTask.Services
{
    public class InventoryRepository: IInventoryRepository
    {
        private readonly TaskDbContext _context;

        public InventoryRepository(TaskDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        public async Task<InventoryItem> GetByIdAsync(int id)
        {
            return await _context.InventoryItems.FindAsync(id);
        }

        public async Task AddAsync(InventoryItem item)
        {
            await _context.InventoryItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, InventoryItem item)
        {
            if (id != item.Id)
            {
                throw new ArgumentException("The ID of the item to update does not match the ID provided.");
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item != null)
            {
                _context.InventoryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}

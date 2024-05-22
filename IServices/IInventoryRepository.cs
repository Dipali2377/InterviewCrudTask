using InterviewCrudTask.Models;

namespace InterviewCrudTask.IServices
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryItem>> GetAllAsync();
        Task<InventoryItem> GetByIdAsync(int id);
        Task AddAsync(InventoryItem item);
        Task UpdateAsync(int id, InventoryItem item);
        Task DeleteAsync(int id);
    }
}

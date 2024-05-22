using InterviewCrudTask.IServices;
using InterviewCrudTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewCrudTask.Services
{
    public class OrderRepository: IOrderRepository
    {
        private readonly TaskDbContext _context;

        public OrderRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Order order)
        {
            if (id != order.Id)
            {
                throw new ArgumentException("The ID of the order to update does not match the ID provided.");
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using InterviewCrudTask.IServices;
using InterviewCrudTask.Models;
using InterviewCrudTask.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace InterviewCrudTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(OrderDto orderitem)
        {
            var orderItem = new Order
            {
                Id = orderitem.Id,
                OrderDate = orderitem.OrderDate
            };
            await _repository.AddAsync(orderItem);
            return CreatedAtAction(nameof(GetOrder), new { id =orderitem.Id }, orderitem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto orderitems)
        {
            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.OrderDate = orderitems.OrderDate;
            await _repository.UpdateAsync(id, existingItem);  
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}

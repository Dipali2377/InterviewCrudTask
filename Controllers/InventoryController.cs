using InterviewCrudTask.IServices;
using InterviewCrudTask.Models;
using InterviewCrudTask.RequestModel;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryRepository _repository;

    public InventoryController(IInventoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
    {
        return Ok(await _repository.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventoryItem>> GetInventoryItem(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<InventoryItem>> PostInventoryItem(InventoryDto inventoryitem)
    {

        var inventoryItem = new InventoryItem
        {
            Id = inventoryitem.Id, // Manually set Productid
            Name = inventoryitem.Name,
            Quantity = inventoryitem.Quantity,
            Price = inventoryitem.Price

        };
        await _repository.AddAsync(inventoryItem);
        return CreatedAtAction(nameof(GetInventoryItem), new { id = inventoryitem.Id }, inventoryitem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutInventoryItem(int id, InventoryDto inventoryitem)
    {
        var existingItem = await _repository.GetByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.Name = inventoryitem.Name;
        existingItem.Quantity = inventoryitem.Quantity;
        existingItem.Price = inventoryitem.Price;

        await _repository.UpdateAsync(id, existingItem);  // Call the update method without assignment
        return NoContent();  // Return NoContent indicating the update was successful
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventoryItem(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}

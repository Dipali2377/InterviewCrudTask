using System;
using System.Collections.Generic;

namespace InterviewCrudTask.Models;

public partial class InventoryItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

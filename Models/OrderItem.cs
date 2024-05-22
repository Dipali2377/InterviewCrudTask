using System;
using System.Collections.Generic;

namespace InterviewCrudTask.Models;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int InventoryItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual InventoryItem InventoryItem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}

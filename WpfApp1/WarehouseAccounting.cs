using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp1
{
    public partial class WarehouseAccounting
    {
        public int ProductId { get; set; }
        public int WhousesId { get; set; }
        public int? Quantity { get; set; }
        public int? Cost { get; set; }

        public virtual Product Product { get; set; }
        public virtual Warehouse Whouses { get; set; }
    }
}

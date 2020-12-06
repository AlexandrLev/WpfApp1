using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp1
{
    public partial class StoreAccounting
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int? Quantity { get; set; }
        public int? Cost { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}

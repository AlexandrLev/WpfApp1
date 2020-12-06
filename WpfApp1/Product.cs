using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp1
{
    public partial class Product
    {
        public Product()
        {
            StoreAccountings = new HashSet<StoreAccounting>();
            WarehouseAccountings = new HashSet<WarehouseAccounting>();
        }

        public int ProductId { get; set; }
        public int Rnumber { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<StoreAccounting> StoreAccountings { get; set; }
        public virtual ICollection<WarehouseAccounting> WarehouseAccountings { get; set; }
    }
}

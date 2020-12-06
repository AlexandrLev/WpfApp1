using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp1
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            WarehouseAccountings = new HashSet<WarehouseAccounting>();
        }

        public int WhousesId { get; set; }
        public int Wnumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }
        public string NameStorekeeper { get; set; }

        public virtual ICollection<WarehouseAccounting> WarehouseAccountings { get; set; }
    }
}

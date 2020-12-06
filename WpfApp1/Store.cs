using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp1
{
    public partial class Store
    {
        public Store()
        {
            StoreAccountings = new HashSet<StoreAccounting>();
        }

        public int StoreId { get; set; }
        public string Sname { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building { get; set; }

        public virtual ICollection<StoreAccounting> StoreAccountings { get; set; }
    }
}

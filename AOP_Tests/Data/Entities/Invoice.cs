using System;

namespace AOP_Tests.Data.Entities {
    public class Invoice {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public Vehicule Vehicule { get; set; }
        public decimal CostPerDay { get; set; }
        public decimal Discount { get; set; }

    }
}

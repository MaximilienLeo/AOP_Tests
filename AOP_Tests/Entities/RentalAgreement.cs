using System;

namespace AOP_Tests {
    public class RentalAgreement {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public Vehicule Vehicule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
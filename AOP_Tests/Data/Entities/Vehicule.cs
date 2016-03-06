using System;

namespace AOP_Tests.Data.Entities {
    public class Vehicule {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Size Size { get; set; }
        public string Vin { get; set; }
    }
}
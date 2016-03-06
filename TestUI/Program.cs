using System;
using AOP_Tests.Business;
using AOP_Tests.Data;
using AOP_Tests.Data.Entities;

namespace TestUI {
    class Program {
        static void Main(string[] args) {
            SimulateAddingPoints();

            Console.WriteLine();
            Console.WriteLine(" ***");
            Console.WriteLine();

            SimulateRemovingPoints();

            Console.WriteLine();
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void SimulateAddingPoints() {
            var dataService = new FakeLoyaltyDataService();
            var service = new LoyaltyAccrualService(dataService);

            var rentalAgreement = new RentalAgreement {
                Customer = new Customer {
                    Id = Guid.NewGuid(),
                    Name = "Test name 1",
                    DateOfBirth = new DateTime(1980, 2, 10),
                    DriversLicense = "RR123456"
                },
                Vehicule = new Vehicule {
                    Id = new Guid(),
                    Make = "Honda",
                    Model = "Accord",
                    Size = Size.Compact,
                    Vin = "1HABBC123"
                },
                StartDate = DateTime.Now.AddDays(-3),
                EndDate = DateTime.Now
            };

            service.Accrue(rentalAgreement);
        }

        private static void SimulateRemovingPoints() {
            var dataService = new FakeLoyaltyDataService();
            var service = new LoyaltyRedemptionService(dataService);

            var invoice = new Invoice {
                Customer = new Customer {
                    Id = Guid.NewGuid(),
                    Name = "Test name 2",
                    DateOfBirth = new DateTime(1977, 4, 15),
                    DriversLicense = "RR009911"
                },
                Vehicule = new Vehicule {
                    Id = new Guid(),
                    Make = "Cadillac",
                    Model = "Sedan",
                    Size = Size.Luxury,
                    Vin = "2BDI"
                },
                CostPerDay = 29.95m,
                Id = Guid.NewGuid()
            };

            service.Redeem(invoice, 3);
        }
    }
}

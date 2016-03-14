using System;
using System.Transactions;
using AOP_Tests.Data;
using AOP_Tests.Data.Entities;

namespace AOP_Tests.Business {
    public class LoyaltyAccrualService : ILoyaltyAccrualService {
        readonly ILoyaltyDataService _loyaltyDataService;

        public LoyaltyAccrualService(ILoyaltyDataService service) {
            _loyaltyDataService = service;
        }

        public void Accrue(RentalAgreement agreement) {
            // Add Defensive Programming
            if (agreement == null) {
                throw new ArgumentNullException(nameof(agreement));
            }

            // Add Logging
            Console.WriteLine($"Accrue : {DateTime.Now}");
            Console.WriteLine($"Customer: {agreement.Customer.Id}");
            Console.WriteLine($"Vehicule: {agreement.Vehicule.Id}");

            // Add Transaction
            using (var scope = new TransactionScope()) {
                try {
                    var rentalTimeSpan = agreement.EndDate.Subtract(agreement.StartDate);
                    var numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
                    var pointsPerDay = 1;

                    if (agreement.Vehicule.Size >= Size.Luxury) {
                        pointsPerDay = 2;
                    }

                    var points = numberOfDays * pointsPerDay;
                    _loyaltyDataService.AddPoints(agreement.Customer.Id, points);
                    scope.Complete();
                } catch {
                    throw;
                }
            }

            // Add Logging
            Console.WriteLine($"Accrue complete: {DateTime.Now}");
        }
    }
}

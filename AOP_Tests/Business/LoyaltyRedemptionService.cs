using System;
using System.Transactions;
using AOP_Tests.Data;
using AOP_Tests.Data.Entities;

namespace AOP_Tests.Business {
    public class LoyaltyRedemptionService : ILoyaltyRedemptionService {
        readonly ILoyaltyDataService _loyaltyDataService;

        public LoyaltyRedemptionService(ILoyaltyDataService service) {
            _loyaltyDataService = service;
        }

        public void Redeem(Invoice invoice, int numberOfDays) {
            // Add Defensive Programming
            if (invoice == null) {
                throw new ArgumentNullException(nameof(invoice));
            }

            if (numberOfDays <= 0) {
                throw new ArgumentException(nameof(numberOfDays));
            }

            // Add Logging
            Console.WriteLine($"Redeem : {DateTime.Now}");
            Console.WriteLine($"Invoice: {invoice.Id}");

            // Add Transaction
            using (var scope = new TransactionScope()) {
                try {
                    var pointsPerDay = 10;
                    if (invoice.Vehicule.Size >= Size.Luxury) {
                        pointsPerDay = 15;
                    }

                    var points = numberOfDays * pointsPerDay;
                    _loyaltyDataService.SubtractPoints(invoice.Customer.Id, points);
                    invoice.Discount = numberOfDays * invoice.CostPerDay;
                    scope.Complete();
                } catch {
                    throw;
                }
            }

            // Add Logging
            Console.WriteLine($"Redeem complete: {DateTime.Now}");
        }
    }
}

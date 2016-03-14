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
                // Add Retry logic
                var retries = 3;
                var succeded = false;
                while (!succeded) {

                    try {
                        // Business logic
                        var pointsPerDay = 10;
                        if (invoice.Vehicule.Size >= Size.Luxury) {
                            pointsPerDay = 15;
                        }

                        var points = numberOfDays * pointsPerDay;
                        _loyaltyDataService.SubtractPoints(invoice.Customer.Id, points);
                        invoice.Discount = numberOfDays * invoice.CostPerDay;
                        // Business logic

                        // Complete transaction
                        scope.Complete();
                        succeded = true;

                        // Add Logging
                        Console.WriteLine($"Redeem complete: {DateTime.Now}");

                    } catch {
                        // Don't rethrow until the limit is reached
                        if (retries >= 0) {
                            retries--;
                        } else {
                            throw;
                        }
                    }
                }
            }
        }
    }
}

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

            // Add More exception Handling
            try {
                // Add Transaction
                using (var scope = new TransactionScope()) {
                    // Add Retry logic
                    var retries = 3;
                    var succeded = false;
                    while (!succeded) {

                        try {

                            // Business logic
                            var rentalTimeSpan = agreement.EndDate.Subtract(agreement.StartDate);
                            var numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
                            var pointsPerDay = 1;

                            if (agreement.Vehicule.Size >= Size.Luxury) {
                                pointsPerDay = 2;
                            }

                            var points = numberOfDays * pointsPerDay;
                            _loyaltyDataService.AddPoints(agreement.Customer.Id, points);
                            // Business logic

                            // Complete transaction
                            scope.Complete();
                            succeded = true;

                            // Add Logging
                            Console.WriteLine($"Accrue complete: {DateTime.Now}");

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
            } catch (Exception ex) {
                // Some exception handling logic. The Book code doesn't work. Something missing?
                //if (!ExceptionHandler.Handle(ex))
                //    throw;
            }
        }
    }
}

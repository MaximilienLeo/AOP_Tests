using System;
using AOP_Tests.Data;
using AOP_Tests.Data.Entities;

namespace AOP_Tests.Business {
    public class LoyaltyAccrualService : ILoyaltyAccrualService {
        readonly ILoyaltyDataService _loyaltyDataService;

        public LoyaltyAccrualService(ILoyaltyDataService service) {
            _loyaltyDataService = service;
        }
     
        public void Accrue(RentalAgreement agreement) {
            var rentalTimeSpan = agreement.EndDate.Subtract(agreement.StartDate);
            var numberOfDays = (int)Math.Floor(rentalTimeSpan.TotalDays);
            var pointsPerDay = 1;

            if (agreement.Vehicule.Size >= Size.Luxury) {
                pointsPerDay = 2;
            }

            var points = numberOfDays * pointsPerDay;
            _loyaltyDataService.AddPoints(agreement.Customer.Id, points);
        }
    }
}

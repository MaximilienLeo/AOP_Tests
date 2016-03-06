﻿using AOP_Tests.Data;
using AOP_Tests.Data.Entities;

namespace AOP_Tests.Business {
    public class LoyaltyRedemptionService : ILoyaltyRedemptionService {
        readonly ILoyaltyDataService _loyaltyDataService;

        public LoyaltyRedemptionService(ILoyaltyDataService service) {
            _loyaltyDataService = service;
        }

        public void Redeem(Invoice invoice, int numberOfDays) {
            var pointsPerDay = 10;
            if (invoice.Vehicule.Size >= Size.Luxury) {
                pointsPerDay = 15;
            }

            var points = numberOfDays * pointsPerDay;
            _loyaltyDataService.SubtractPoints(invoice.Customer.Id, points);
            invoice.Discount = numberOfDays * invoice.CostPerDay;
        }
    }
}

using AOP_Tests.Data.Entities;

namespace AOP_Tests.Business {
    public interface ILoyaltyRedemptionService {
        void Redeem(Invoice invoice, int numberOfDays);
    }
}

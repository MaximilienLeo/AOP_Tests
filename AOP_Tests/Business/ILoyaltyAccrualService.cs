using AOP_Tests.Data.Entities;

namespace AOP_Tests {
    public interface ILoyaltyAccrualService {
        void Accrue(RentalAgreement agreement);
    }
}

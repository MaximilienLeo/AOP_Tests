using System;

namespace AOP_Tests.Data {
    public class FakeLoyaltyDataService : ILoyaltyDataService {
        public void AddPoints(Guid customerId, int points) {
            Console.WriteLine($"Adding {points} points for customer '{customerId}'");
        }

        public void SubtractPoints(Guid customerId, int points) {
            Console.WriteLine($"Subtracting {points} points for customer '{customerId}'");
        }
    }
}

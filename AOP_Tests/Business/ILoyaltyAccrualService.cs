using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP_Tests {
    interface ILoyaltyAccrualService {
        void Accrue(RentalAgreement agreement);
    }
}

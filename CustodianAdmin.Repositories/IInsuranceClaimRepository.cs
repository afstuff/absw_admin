using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;


namespace CustodianAdmin.Repositories
{
    public interface IInsuranceClaimRepository:IRepository<InsuranceClaim,Int32?>
    {
        IList<InsuranceClaim> InsuranceClaimBills();

    }
}

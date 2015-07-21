using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;


namespace CustodianAdmin.Repositories
{
    public interface IInsurancePremRepository:IRepository<InsurancePrem,Int32?>
    {
        IList<InsurancePrem> InsurancePremBills();
    }
}

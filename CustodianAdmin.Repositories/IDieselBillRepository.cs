using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;

namespace CustodianAdmin.Repositories
{
    public interface IDieselBillRepository:IRepository<DieselBill,Int32?>
    {
        IList<DieselBill> DieselBills();

    }
}

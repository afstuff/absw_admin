using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;

namespace CustodianAdmin.Repositories
{
    public interface ITelephoneBillRepository:IRepository<TelephoneBill,Int32?>
    {
        IList<TelephoneBill> TelephoneBills();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;



namespace CustodianAdmin.Repositories
{
    public interface IRepairsBillRepository:IRepository<RepairsBill,Int32?>
    {
        IList<RepairsBill> RepairBills();

    }
}

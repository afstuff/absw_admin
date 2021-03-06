﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;

namespace CustodianAdmin.Repositories
{
    public interface IElectricityBillRepository : IRepository<ElectricityBill, Int32?>
    {
        IList<ElectricityBill> ElectricBills();

    }
}

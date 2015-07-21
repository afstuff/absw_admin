using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class ElectricityBill
    {
        public virtual int? ebId { get; set; }
        public virtual string TransClass { get; set; }
        public virtual string TransId { get; set; }
        public virtual string MeterNo { get; set; }
        public virtual string AccountNo { get; set; }
        public virtual DateTime TransDate { get; set; }
        public virtual string BranchCode { get; set; }
        public virtual string Department { get; set; }
        public virtual Decimal TransAmount { get; set; }
        public virtual string PeriodPaidFor { get; set; }
        public virtual string EntryFlag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }
    }
}
 
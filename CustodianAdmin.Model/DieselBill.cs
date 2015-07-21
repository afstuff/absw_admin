using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class DieselBill
    {
        public virtual int? dbId { get; set; }
        public virtual string TransClass { get; set; }
        public virtual string TransId { get; set; }
        public virtual string TransNo { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual DateTime TransDate { get; set; }
        public virtual string BranchCode { get; set; }
        public virtual string Department { get; set; }
        public virtual Decimal Quantity { get; set; }
        public virtual Decimal UnitPrice { get; set; }
        public virtual Decimal TransAmount { get; set; }
        public virtual string SupplyCompany { get; set; }
        public virtual string TransDescription { get; set; }
        public virtual string EntryFlag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }


    }
}

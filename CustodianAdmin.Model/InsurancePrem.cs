using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class InsurancePrem
    {
        public virtual int? ibId { get; set; }
        public virtual string TransNo { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual DateTime TransDate { get; set; }
        public virtual string BranchCode { get; set; }
        public virtual string Department { get; set; }
        public virtual string PolicyNo { get; set; }
        public virtual string BrokerName { get; set; }
        public virtual string InsurerName { get; set; }
        public virtual string CoInsurer1 { get; set; }
        public virtual string CoInsurer2 { get; set; }
        public virtual string CoInsurer3 { get; set; }
        public virtual string CoInsurer4 { get; set; }
        public virtual string CoInsurer5 { get; set; }
        public virtual string TransDescription { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual Decimal SumInsured { get; set; }
        public virtual Decimal PremiumAmt { get; set; }
        public virtual string EntryFlag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }

    }
}
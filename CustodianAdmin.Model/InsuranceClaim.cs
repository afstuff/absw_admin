using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class InsuranceClaim
    {
        public virtual int? cbId { get; set; }
        public virtual string TransNo { get; set; }
        public virtual string TransactionType { get; set; }
        public virtual DateTime TransDate { get; set; }
        public virtual string BranchCode { get; set; }
        public virtual string Department { get; set; }
        public virtual string PolicyNo { get; set; }
        public virtual string ClaimNo { get; set; }
        public virtual string BrokerName { get; set; }
        public virtual string InsurerName { get; set; }
        public virtual string TransDescription { get; set; }
        public virtual DateTime LossDate { get; set; }
        public virtual Decimal ClaimRequested { get; set; }
        public virtual Decimal ClaimPaid { get; set; }
        public virtual string EntryFlag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }

    }
}
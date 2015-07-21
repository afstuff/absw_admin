using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class Branch
    {
        public virtual int? bId { get; set; }
        public virtual string BranchId { get; set; }
        public virtual string BranchNo { get; set; }
        public virtual string BranchName { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string Address3 { get; set; }
        public virtual string Phone1 { get; set; }
        public virtual string Phone2 { get; set; }
        public virtual string Fax1 { get; set; }
        public virtual string Fax2 { get; set; }
        public virtual string Manager { get; set; }
        public virtual string LocationNo { get; set; }
        public virtual string LocationName { get; set; }
        public virtual string Flag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }

    }
}
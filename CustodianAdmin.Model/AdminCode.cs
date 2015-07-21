using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class AdminCode
    {
        public virtual int? icId { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ItemDesc { get; set; }
        public virtual string TransType { get; set; }
        public virtual string Branch { get; set; }
        public virtual string Dept { get; set; }
        public virtual string Flag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }

    }
}

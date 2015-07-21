using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class Department
    {
        public virtual int? dId { get; set; }
        public virtual string DeptID { get; set; }
        public virtual string DeptNo { get; set; }
        public virtual string LongDesc { get; set; }
        public virtual string ShortDesc { get; set; }
        public virtual string Flag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class MotorCar
    {
        public virtual int? mId { get; set; }
        public virtual string Make { get; set; }
        public virtual string Type { get; set; }
        public virtual string Flag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }

    }
}

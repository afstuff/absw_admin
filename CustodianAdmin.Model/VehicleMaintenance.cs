using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustodianAdmin.Model
{
    public class VehicleMaintenance
    {
        public virtual int? vmId { get; set; }
        public virtual string TransClass { get; set; }
        public virtual string TransId { get; set; }
        public virtual string VehicleNo { get; set; }
        public virtual string VehicleType { get; set; }
        public virtual DateTime TransDate { get; set; }
        public virtual string MaintenanceType { get; set; }
        public virtual string UserName { get; set; }
        public virtual string BranchCode { get; set; }
        public virtual string Department { get; set; }
        public virtual Decimal TransAmount { get; set; }
        public virtual string Description { get; set; }
        public virtual string ServiceCompany { get; set; }
        public virtual string EntryFlag { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual string OperatorId { get; set; }

    }
}

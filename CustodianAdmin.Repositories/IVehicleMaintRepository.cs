using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;

namespace CustodianAdmin.Repositories
{
    public interface IVehicleMaintRepository:IRepository<VehicleMaintenance, Int32?>
    {
        IList<VehicleMaintenance> MaintenanceDetails();

    }
}

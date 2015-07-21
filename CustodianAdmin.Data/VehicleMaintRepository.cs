using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using CustodianAdmin.Model;
using CustodianAdmin.Repositories;
using NHibernate;

namespace CustodianAdmin.Data
{
    public class VehicleMaintRepository:IVehicleMaintRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(VehicleMaintenance saveObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.FlushMode = FlushMode.Commit;
                    session.SaveOrUpdate(saveObj);
                    trans.Commit();
                    session.Flush();
                    //}
                }
            }
        }
        public void Delete(VehicleMaintenance delObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(delObj);
                    trans.Commit();
                }
            }
        }
        public IList<VehicleMaintenance> MaintenanceDetails()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<VehicleMaintenance>()

                                     .List<VehicleMaintenance>();

                return pDet;
            }
        }
        public VehicleMaintenance GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<VehicleMaintenance>(id);
            }
        }
        public VehicleMaintenance GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from VehicleMaintenance i where i.vmId = " + keys[0]
                              + " and i.VehicleNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (VehicleMaintenance)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }

        public String GetVehicleInfo(String _admincode)
        {
            //queries the generic admincodes table and extract info for the vehicles only
            string query = "SELECT * "
                          + "FROM CiFn_GetMiscAdminCodeDetails('001','"
                          + _admincode + "',NULL,NULL,NULL)";

            return GetDataSet(query).GetXml();
        }

        private static DataSet GetDataSet(string qry)
        {
            using (var session = GetSession())
            {
                using (var conn = session.Connection as SqlConnection)
                {
                    var adapter = new SqlDataAdapter(qry, conn);
                    var dataSet = new System.Data.DataSet();

                    adapter.Fill(dataSet);

                    return dataSet;
                }
            }
        }

    }
}

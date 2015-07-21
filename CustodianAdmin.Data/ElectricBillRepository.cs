using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;
using CustodianAdmin.Repositories;
using NHibernate;


namespace CustodianAdmin.Data
{
    public class ElectricBillRepository:IElectricityBillRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(ElectricityBill saveObj)
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
        public void Delete(ElectricityBill delObj)
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
        public IList<ElectricityBill> ElectricBills()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<ElectricityBill>()

                                     .List<ElectricityBill>();

                return pDet;
            }
        }
        public ElectricityBill GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<ElectricityBill>(id);
            }
        }
        public ElectricityBill GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from ElectricityBill i where i.ebId = " + keys[0]
                              + " and i.MeterNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (ElectricityBill)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }

    }
}

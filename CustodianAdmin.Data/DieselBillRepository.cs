using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;
using CustodianAdmin.Repositories;
using NHibernate;


namespace CustodianAdmin.Data
{
    public class DieselBillRepository:IDieselBillRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(DieselBill saveObj)
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
        public void Delete(DieselBill delObj)
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
        public IList<DieselBill> DieselBills()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<DieselBill>()

                                     .List<DieselBill>();

                return pDet;
            }
        }
        public DieselBill GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<DieselBill>(id);
            }
        }
        public DieselBill GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from DieselBill i where i.dbId = " + keys[0]
                              + " and i.TransNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (DieselBill)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }

    }
}

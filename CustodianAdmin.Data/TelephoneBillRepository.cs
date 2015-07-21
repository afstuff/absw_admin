using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;
using CustodianAdmin.Repositories;
using NHibernate;



namespace CustodianAdmin.Data
{
    public class TelephoneBillRepository:ITelephoneBillRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(TelephoneBill saveObj)
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
        public void Delete(TelephoneBill delObj)
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
        public IList<TelephoneBill> TelephoneBills()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<TelephoneBill>()

                                     .List<TelephoneBill>();

                return pDet;
            }
        }
        public TelephoneBill GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<TelephoneBill>(id);
            }
        }
        public TelephoneBill GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from TelephoneBill i where i.tbId = " + keys[0]
                              + " and i.TelephoneNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (TelephoneBill)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }


    }
}

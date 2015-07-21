using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;
using CustodianAdmin.Repositories;
using NHibernate;

namespace CustodianAdmin.Data
{
     public class InsurancePremRepository:IInsurancePremRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(InsurancePrem saveObj)
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
        public void Delete(InsurancePrem delObj)
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
        public IList<InsurancePrem> InsurancePremBills()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<InsurancePrem>()

                                     .List<InsurancePrem>();

                return pDet;
            }
        }
        public InsurancePrem GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<InsurancePrem>(id);
            }
        }
        public InsurancePrem GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from InsurancePrem i where i.ibId = " + keys[0]
                              + " and i.TransNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (InsurancePrem)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }


    }
}

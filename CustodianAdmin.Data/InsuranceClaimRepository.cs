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
    public class InsuranceClaimRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(InsuranceClaim saveObj)
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
        public void Delete(InsuranceClaim delObj)
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
        public IList<InsuranceClaim> InsuranceClaimBills()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<InsuranceClaim>()

                                     .List<InsuranceClaim>();

                return pDet;
            }
        }
        public InsuranceClaim GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<InsuranceClaim>(id);
            }
        }
        public InsuranceClaim GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from InsuranceClaim i where i.cbId = " + keys[0]
                              + " and i.TransNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (InsuranceClaim)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }

        public String GetPolicyInfo(String _polnum)
        {

            string query = "SELECT * "
                          + "FROM CiFn_GetAdmPolicyInfo('" + _polnum + "',NULL,NULL,NULL)";

            return GetDataSet(query).GetXml();

        }

        /// <summary>
        /// this returns a dataset to be converted to XML for serialization into a Json object 
        /// used to retrieve data from the back end to the html page using jQuery 
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
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

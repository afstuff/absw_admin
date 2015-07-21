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
    public class RepairsBillRepository
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public void Save(RepairsBill saveObj)
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
        public void Delete(RepairsBill delObj)
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
        public IList<RepairsBill> RepairsBills()
        {
            using (var session = GetSession())
            {
                var pDet = session.CreateCriteria<RepairsBill>()

                                     .List<RepairsBill>();

                return pDet;
            }
        }
        public RepairsBill GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<RepairsBill>(id);
            }
        }
        public RepairsBill GetById(String _key)
        {
            //the _key is an array of string values (3). Split into individual values and fill the parameters
            Char[] seperator = new char[] { ',' };
            string[] keys = _key.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            string hqlOptions = "from RepairsBill i where i.rbId = " + keys[0]
                              + " and i.TransNo = '" + keys[1] + "'";

            using (var session = GetSession())
            {

                return (RepairsBill)session.CreateQuery(hqlOptions).UniqueResult();
            }
        }

        public IList<AdminCode> GetAdminCodes(String _classcode)
        {

            string hqlOptions = "from AdminCode i where i.ClassCode = '" + _classcode + "'";

            using (var session = GetSession())
            {
                return session.CreateQuery(hqlOptions).List<AdminCode>();

            }
        }

        public String GetNextSerialNumber(string sys_id
                               , String sys_type
                               , String sys_branch
                               , String sys_year
                               , String sys_prefix
                               , String sys_out_int
                               , String sys_out_char)
        {
            var session = GetSession();
            session.BeginTransaction();
            IDbCommand command = session.Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "CiSP_GetAdminSerialNo";

            IDbDataParameter inputparameter1 = command.CreateParameter();
            inputparameter1.ParameterName = "@PARAM_SYS_ID";
            inputparameter1.DbType = DbType.String;
            inputparameter1.Size = 5;
            inputparameter1.Direction = ParameterDirection.Input;
            inputparameter1.Value = sys_id;

            command.Parameters.Add(inputparameter1);

            IDbDataParameter inputparameter2 = command.CreateParameter();
            inputparameter2.ParameterName = "@PARAM_SYS_TYPE";
            inputparameter2.DbType = DbType.String;
            inputparameter2.Direction = ParameterDirection.Input;
            inputparameter2.Size = 5;
            inputparameter2.Value = sys_type;
            command.Parameters.Add(inputparameter2);

            IDbDataParameter inputparameter3 = command.CreateParameter();
            inputparameter3.ParameterName = "@PARAM_SYS_BRANCH";
            inputparameter3.DbType = DbType.String;
            inputparameter3.Direction = ParameterDirection.Input;
            inputparameter3.Size = 5;
            inputparameter3.Value = sys_branch;
            command.Parameters.Add(inputparameter3);

            IDbDataParameter inputparameter4 = command.CreateParameter();
            inputparameter4.ParameterName = "@PARAM_SYS_YEAR";
            inputparameter4.DbType = DbType.String;
            inputparameter4.Direction = ParameterDirection.Input;
            inputparameter4.Size = 10;
            inputparameter4.Value = sys_year;
            command.Parameters.Add(inputparameter4);

            IDbDataParameter inputparameter5 = command.CreateParameter();
            inputparameter5.ParameterName = "@PARAM_SYS_PREFIX";
            inputparameter5.DbType = DbType.String;
            inputparameter5.Size = 25;
            inputparameter5.Direction = ParameterDirection.Input;
            inputparameter5.Value = sys_prefix;
            command.Parameters.Add(inputparameter5);

            IDbDataParameter inputparameter6 = command.CreateParameter();
            inputparameter6.ParameterName = "@PARAM_OUT_INT";
            inputparameter6.DbType = DbType.String;
            inputparameter6.Direction = ParameterDirection.Input;
            inputparameter6.Size = 20;
            inputparameter6.Value = sys_out_int;
            command.Parameters.Add(inputparameter6);

            IDbDataParameter outparameter = command.CreateParameter();
            outparameter.ParameterName = "@PARAM_OUT_CHAR";
            outparameter.DbType = DbType.String;
            outparameter.Size = 20;
            outparameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(outparameter);

            session.Transaction.Enlist(command);
            command.ExecuteNonQuery();

            //retrieve value from out parameter
            IDbDataParameter outparameter4valu = command.CreateParameter();

            outparameter4valu = (IDbDataParameter)command.Parameters["@PARAM_OUT_CHAR"];
            string outval = (string)outparameter4valu.Value;

            session.Transaction.Commit();
            return outval;
        }



    }
}

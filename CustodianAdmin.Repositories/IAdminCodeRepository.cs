using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using CustodianAdmin.Model;


namespace CustodianAdmin.Repositories
{
    public interface IAdminCodeRepository:IRepository<AdminCode,Int32?>
    {
        IList GetByIds(String ClassId, String ItemCode);

    }
}

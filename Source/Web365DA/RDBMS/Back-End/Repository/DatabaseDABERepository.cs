using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Back_End.Repository
{
    public class DatabaseDABERepository : BaseBE<tblAdvertise>, IDatabaseDABERepository
    {
        public bool ExecQuery(string query)
        {
            web365db.Database.ExecuteSqlCommand(query);

            return false;
        }
    }
}

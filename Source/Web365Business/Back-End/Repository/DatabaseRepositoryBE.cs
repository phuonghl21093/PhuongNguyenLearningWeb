using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Back_End.IRepository;
using Web365DA.RDBMS.Back_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Back_End.Repository
{
    public class DatabaseRepositoryBE : BaseBE<object>, IDatabaseRepositoryBE
    {
        private readonly IDatabaseDABERepository databaseDABERepository;

        public DatabaseRepositoryBE(IDatabaseDABERepository _databaseDABERepository)
        {
            databaseDABERepository = _databaseDABERepository;
        }

        public bool ExecQuery(string query)
        {
            return databaseDABERepository.ExecQuery(query);
        }
    }
}

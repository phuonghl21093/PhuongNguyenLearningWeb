using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Business.Front_End.IRepository;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365Business.Front_End.Repository
{
    public class OtherRepositoryFE : BaseFE, IOtherRepositoryFE
    {
        private readonly IOtherDAFERepository otherDAFERepository;

        public OtherRepositoryFE(IOtherDAFERepository _otherDAFERepository)
        {
            otherDAFERepository = _otherDAFERepository;
        }

        public bool AddContact(tblContact contact)
        {
            return otherDAFERepository.AddContact(contact);   
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web365Base;
using Web365Domain;

namespace Web365DA.RDBMS.Front_End.IRepository
{
    public interface IOtherDAFERepository
    {
        bool AddContact(tblContact contact);
    }
}

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
    public class ManufacturerRepositoryBE : BaseBE<tblManufacturer>, IManufacturerRepositoryBE
    {
        private readonly IManufacturerDABERepository manufacturerDABERepository;

        public ManufacturerRepositoryBE(IManufacturerDABERepository _manufacturerDABERepository)
            : base(_manufacturerDABERepository)
        {
            manufacturerDABERepository = _manufacturerDABERepository;
        }

        /// <summary>
        /// function get all data tblManufacturer
        /// </summary>
        /// <returns></returns>
        public List<ProductManufacturerItem> GetList(out int total, string name, int currentRecord, int numberRecord, string propertyNameSort, bool descending, bool isDelete = false)
        {
            return manufacturerDABERepository.GetList(out total, name, currentRecord, numberRecord, propertyNameSort, descending, isDelete);
        }

    }
}

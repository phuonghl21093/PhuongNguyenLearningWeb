using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Web365Base;
using Web365DA.RDBMS.Front_End.IRepository;
using Web365Domain;
using Web365Utility;

namespace Web365DA.RDBMS.Front_End.Repository
{
    public class AdvertiesDAFERepository : BaseFE, IAdvertiesDAFERepository
    {
        public AdvertiesItem GetItemById(int id)
        {
            var advertise = new AdvertiesItem();

            var result = (from p in web365db.tblAdvertise
                          where p.ID == id && p.IsShow == true && p.IsDeleted == false
                          orderby p.ID descending
                          select p).FirstOrDefault();

            if (result != null)
            {
                advertise = new AdvertiesItem()
                {
                    ID = result.ID,
                    DateCreated = result.DateCreated,
                    DateUpdated = result.DateUpdated,
                    Detail = result.Detail,
                    IsShow = result.IsShow,
                    ListPicture = result.tblAdverties_Picture_Map.OrderByDescending(p => p.DisplayOrder).Select(p => new PictureItem()
                    {
                        ID = p.PictureID ?? 0,
                        FileName = p.PictureID.HasValue ? p.tblPicture.FileName: string.Empty
                    }).ToList()
                };
            }

            return advertise;
        }

        public AdvertiesItem GetItemByLink(string link)
        {
            var advertise = new AdvertiesItem();

            var result = (from p in web365db.tblAdvertise
                          where link.Contains(p.Link) && p.IsShow == true && p.IsDeleted == false
                          orderby p.ID descending
                          select p).FirstOrDefault();

            if (result != null)
            {
                advertise = new AdvertiesItem()
                {
                    ID = result.ID,
                    DateCreated = result.DateCreated,
                    DateUpdated = result.DateUpdated,
                    Detail = result.Detail,
                    IsShow = result.IsShow,
                    ListPicture = result.tblAdverties_Picture_Map.OrderByDescending(p => p.DisplayOrder).Select(p => new PictureItem()
                    {
                        ID = p.PictureID ?? 0,
                        Link = p.PictureID.HasValue ? p.tblPicture.Link : string.Empty,
                        FileName = p.PictureID.HasValue ? p.tblPicture.FileName : string.Empty

                    }).ToList()
                };
            }

            return advertise;
        }
    }
}

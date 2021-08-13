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
    public class AdvertiesPictureMapRepositoryBE : BaseBE<object>, IAdvertiesPictureMapRepositoryBE
    {
        private readonly IAdvertiesPictureMapDABERepository advertiesPictureMapDABERepository;

        public AdvertiesPictureMapRepositoryBE(IAdvertiesPictureMapDABERepository _advertiesPictureMapDABERepository)
        {
            advertiesPictureMapDABERepository = _advertiesPictureMapDABERepository;
        }

        public void ResetPictureOfAdverties(int advId, int[] pictureId)
        {
            advertiesPictureMapDABERepository.ResetPictureOfAdverties(advId, pictureId);
        }
    }
}

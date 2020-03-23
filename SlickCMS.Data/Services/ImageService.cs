using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class ImageService : BaseService<Image>, IImageService
    {
        public ImageService() { }
        public ImageService(SlickCMSContext context) : base(context) { }

        // TODO
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelSnap_PassionProject.Models.ViewModels
{
    public class GalleryEdit
    {
        public GalleryEdit()
        {
        }
        // I WANT TO INCLUDE PHOTOGRAPHER MODEL IN THE FUTURE THAT WILL BE ASSOCITAED WITH GALLERIES AND IMAGES
        public virtual Gallery gallery { get; set; }

        public virtual Image image { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixelSnap_PassionProject.Models.ViewModels
{
    public class ImageEdit
    {
        public ImageEdit()
        {

        }
        //HERE I WANT TO PUT CATEGORIES AND TAGS IN THE FUTURE THAT WILL BE ASSOCITAED TO IMAGES ANG GALLERY
        public virtual Image Image { get; set; }

        public IEnumerable<Gallery> gallery { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
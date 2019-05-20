using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PixelSnap_PassionProject.Models
{
    public class Gallery
    {
        //GALLERYID
        [Key, ScaffoldColumn(false)]
        public int GalleryID { get; set; }

        //GALLERY NAME
        [Required, StringLength(255), Display(Name = "Gallery Name")]
        public string GalleryName { get; set; }

        //GALLERY DESCRIPTION
        [StringLength(int.MaxValue), Display(Name = "Gallery Description")]
        public string GalleryDescription { get; set; }

        //[Required, Display(Name = "Date Created"), DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }

        //THIS WILL SET THE FEATURED IMAGE
        public string SetPic { get; set; }

        //THIS PART IS SAVING THE IMAGE PATH OF UPLOADED IMAGE
        public string ImagePath { get; set; }

        //GALLERY CATEGORY
        public string GalleryCatergory { get; set; }

        //ONE GALLERY TO MANY IMAGES
        public virtual ICollection<Image> Images { get; set; }

    }
}
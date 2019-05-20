using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PixelSnap_PassionProject.Models
{
    public class Image
    {
        //IMAGEID
        [Key, ScaffoldColumn(false)]
        public int ImageID { get; set; }

        //IMAGE NAME
        [Required, StringLength(255), Display(Name = "Image Name")]
        public string ImageName { get; set; }

        //IMAGE DESCRIPTION
        [StringLength(int.MaxValue), Display(Name = "Image Description")]
        public string ImageDescription { get; set; }

        //GETTING THE DATETIME
        public DateTime DateCreated { get; set; }

        //IMAGE CATEGORY
        public string ImageCategory { get; set; }

        //THIS WILL SAVE THE IMAGE PATH OF UPLOADED IMAGE
        public string ImagePath { get; set; }

        //THIS IS FRO CHECKBOX CONFIRMING IS THE IMAGE IS SET TO BE FEATURED
        public bool IsFeatured { get; set; }

        //ONE GALLERY TO MANY IMAGES
        public virtual Gallery gallery { get; set; }
    }
}
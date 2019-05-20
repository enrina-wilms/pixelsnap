using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PixelSnap_PassionProject.Models
{
    public class PixelSnapCMS : DbContext
    {
        public PixelSnapCMS()
        {

        }

        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
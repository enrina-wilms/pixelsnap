using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using PixelSnap_PassionProject.Models;
using PixelSnap_PassionProject.Models.ViewModels;

namespace PixelSnap_PassionProject.Controllers
{
    public class ImageController : Controller
    {
        private PixelSnapCMS db = new PixelSnapCMS();

        // GET: Image
        public ActionResult Index()
        {
            //WHEN THE SHARE IMAGE IS CLICK ON THE NAVIGATION THIS WILL REDIRECT TO LIST.CSHTML VIEW PAGE
            //WHERE IT WILL SHOW AND DISPLAY THE IMAGES THAT IS FEATURED AND BELOW THAT ARE 
            //LIST OF ALL UPLOADED IMAGES PULLED OUT FROM THE DATABASE
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            //LIST VIEW WILL DISPLAY LISTS OF IMAGES UPLOADED AND SAVE TO MY IMAGES MODEL/IMAGES TABLE
            return View(db.Images.ToList());
        }

        public ActionResult Create()
        {
            //GETTING A LIST OF GALLERIES SAVED TO THE DATABASE TO PULL OUT DATA THAT IS NEEDED FOR UPLOADING IMAGE SINCE I NEED THE GALLERYNAME AND GALLERYid
            return View(db.Galleries.ToList());
        }

        [HttpPost]
        //AGAIN THE HTTPPOSTFILEBASE IS NEEDED IS TO ACCESS THE UPLOADED FILES
        public ActionResult Create(string ImageName_new, string ImageDescription_new, int? ImageGallery_New, string ImagePath, string ImageCategory, bool ImageFeature_new, Image image, HttpPostedFileBase uploadImage)
        //public ActionResult Create(string ImageName_new, string ImageDescription_new, int? ImageGallery_New, int Haspic, Image image, HttpPostedFileBase uploadImage)
        {
            //QUERY TO INSERT DATA TO IMAGES TABLES WHEN UPLOADING NEW IMAGE
            string query = "insert into Images (ImageName, ImageDescription, gallery_GalleryID, ImagePath, DateCreated, ImageCategory, IsFeatured) values (@name, @description, @galleryID, @path, @date, @category, @featured)";

            if (uploadImage != null) //uploadImage !=null
            {
                //THIS WHERE THE UPLOADED IMAGE WILL BE SAVE
                uploadImage.SaveAs(Server.MapPath("~/ImageUploads/images/") + uploadImage.FileName);

                //THIS VALUE WILL BE SET TO MY IMAGE MODEL WHERE IMAGEPATH IS EQUAL TO uploadImage.FileName
                //THIS WILL BE THE PATH THAT I NEED TO DISPLAY THE IMAGES IN MY IMAGE VIEWS
                ImagePath = uploadImage.FileName;
                
                //ARRAYS OF SQLPARAMETERS SET TO WRITE DATA TO MY IMAGES TABLE
                SqlParameter[] myparams = new SqlParameter[7];
                myparams[0] = new SqlParameter("@name", ImageName_new);
                myparams[1] = new SqlParameter("@description", ImageDescription_new);
                myparams[2] = new SqlParameter("@galleryID", ImageGallery_New);
                myparams[3] = new SqlParameter("@path", ImagePath);
                myparams[4] = new SqlParameter("@date", DateTime.Now);
                myparams[5] = new SqlParameter("@category", ImageCategory);
                myparams[6] = new SqlParameter("@featured", ImageFeature_new);

                //EXECUTING THE QUERY TO INSERT DATA ON THE DATABASE IMAGES TABLE
                db.Database.ExecuteSqlCommand(query, myparams);

                //AFTER RUNNING THE QUERY AND NEW IMAGE IS UPLOADED
                //THIS WILL RETURN TO LIST.CSHTML IN IMAGE VIEW
                return RedirectToAction("List");
            }

            //db.Database.ExecuteSqlCommand(query, myparams);
            return View(image);
        }

        public ActionResult Edit(int id)
        {
            //SELECTING IMAGES THAT WAS SAVE IN MY IMAGES TABLE TO PULL DATA AND EDIT THE IMAGE
            ImageEdit imageeditview = new ImageEdit
            {
                Image = db.Images.Find(id)
            };
            return View(imageeditview);
        }
        [HttpPost]

        public ActionResult Edit(int id, string ImageName_new, string ImageDescription_new, string ImagePath, string ImageCategory, bool ImageFeature_new, Image image, HttpPostedFileBase uploadImage)
        {
            if ((id == null) || (db.Images.Find(id) == null))
            {
                return HttpNotFound();
            }

            //QUERY TO UPDATE THE IMAGE TABLE
            string query = "update Images set ImageName=@name, ImageDescription=@description, ImagePath=@path, ImageCategory=@category, DateCreated=@date, IsFeatured=@featured where ImageID=@id";
            if (uploadImage != null) //uploadImage !=null
            {
                //THE SAME AS CREATE VIEW IF THE USER WANTS TO REUPLOAD AN IMAGE
                uploadImage.SaveAs(Server.MapPath("~/ImageUploads/images/") + uploadImage.FileName);

                ImagePath = uploadImage.FileName;

                SqlParameter[] myparams = new SqlParameter[7];
                myparams[0] = new SqlParameter("@name", ImageName_new);
                myparams[1] = new SqlParameter("@description", ImageDescription_new);
                myparams[2] = new SqlParameter("@path", ImagePath);
                myparams[3] = new SqlParameter("@category", ImageCategory);
                myparams[4] = new SqlParameter("@date", DateTime.Now);
                myparams[5] = new SqlParameter("@id", id);
                myparams[6] = new SqlParameter("@featured", ImageFeature_new);

                //RUNNING AND EXECUTING THE SQL COMMAND TO UPDATE THE IMAGE IN IMAGES TABLE
                db.Database.ExecuteSqlCommand(query, myparams);

                //AFTER UPDATING THIS WILL REDIRECTTO SHOW.CSHTML IN IMAGE VIEW TO DISPLAY THE UPDATED IMAGE
                return RedirectToAction("Show/" + id);
            }
            return View(image);

        }


        public ActionResult Show(int id)
        {
            // A QUERY THE WILL SELECT A SPECIFIC IMAGE IF CLICK IN THE LIST VIEW TO DISPLA DETAILS AND IMAGE
            string query = "select * from Images where ImageID=@id";

            SqlParameter[] myparams = new SqlParameter[1];
            myparams[0] = new SqlParameter("@id", id);

            Image imageShow = db.Images.SqlQuery(query, myparams).FirstOrDefault();

            return View(imageShow);
        }

        public ActionResult Delete(int id, Image image)
        {
            //I WANT TO DELETE THE UPLOADED IMAGE IF I DELETED THE IMAGE ON THE GALLERY BUT IT SAYS ACCESS DENIED
            //string path = Server.MapPath("~/ImageUploads/") + image.ImagePath;
            //if(image.ImagePath == null)
            //{
                //System.IO.File.Delete(path);
            //}

            //QUERY TO DELETE A SPECIFIC IMAGE FROM THE DATABASE
            string query = "delete from Images where ImageID = @id";
            
            //EXECUTING THE SQL QUERY
            db.Database.ExecuteSqlCommand(query, new SqlParameter("@id", id));

            //AFTER DELETING AN IMAGE THIS WILL REDIRECT TO LIST VIEW AGAIN
            return RedirectToAction("List");
        }
        
    }
}
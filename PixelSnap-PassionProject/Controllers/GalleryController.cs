using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using PixelSnap_PassionProject.Models;
using PixelSnap_PassionProject.Models.ViewModels;

namespace PixelSnap_PassionProject.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        private PixelSnapCMS db = new PixelSnapCMS();

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            //GETTING A LIST OG GALLERIES IN THE DATABASE
            IEnumerable<Gallery> galleries = db.Galleries.ToList();
            //THIS WILL DISPLAY AND GO TO VIEWS LIST.CSHTML
            return View(galleries);
        }

        public ActionResult Create()
        {
            //SINCE I AHVEN'T WORKED ON PHOTGRAPHER MODEL AND CONTROLLE
            // I DON;T NEED ANY INFORMATION ON VIEW MODELS GALLERYEDIT.CS
            //GalleryEdit galleryeditview = new GalleryEdit();

            return View();
        }

        [HttpPost]
        //IM NOT SURE IS I NEED TO INCLUDE THE  [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //HTTPPOSTEDFILEBASS CLASS PROVIDES ACCESS TO FILES THAT HAVE BEEN UPLOADED
        public ActionResult Create(string GalleryName_New, string GalleryDescription_New, string GalleryCategory, string ImagePath, string SetPic, Gallery gallery, HttpPostedFileBase uploadImage)
        {
            //QUERY FO INSERTING DATA TO THE DATABASE
            string query = "insert into Galleries (GalleryName, GalleryDescription, DateCreated, GalleryCatergory, ImagePath, SetPic) values (@name, @description, @date, @category, @path, @set)";

            if (uploadImage != null)
            {
                //I DID TRY THE SAME THING AS ON YOUR EXAMPLE AND THE LINK TO A TUTORIAL BUT CHANGING FILENAME GIVES ME ERROR
                //IM A BIT CONFUSED HOW TO DO IT PROPERLY THATS WHY THE FILENAME IS STILL THE SAME WHEN I UPLOADED IT
                // BU TIN THE FUTURE I WILL WANT TO TRY FILE EXTENSION RESTICTIONS AND RENAMEING THE FILE BEFORE SAVING IT TO THE PATH FOLDER
                //THIS IS THE PATH WHERE THE UPLOADED IMAGE WILL BE SAVE
                uploadImage.SaveAs(Server.MapPath("~/ImageUploads/gallery-image/") + uploadImage.FileName);

                //THIS WILL SET THE VALUE OF IMAGEPATH IN THE DATABASE SO
                //I CAN PULL OUT THE IMAGE PATH WHENI NEED IT TO DISPLAY THE IMAGE IN MY VIEWS
                ImagePath = uploadImage.FileName;

                //THIS IS SUPPOSED TO BE CONFIRMING IF THERE'S AN IMAGE UPLOADED FOR THIS GALLERY
                //THE I WILL ONLY HAVE 1 IMAGE SET TO BE UPLOADED FOR THE GALLERY FOR THE SAKE OF
                //DISPLAYING IMAGE FOR LIST.CSHTML FOR MY GALLERY
                // ITS LIKE A PORFILE PIC OR FEATURED IMAGE
                SetPic = "set";

                //SETTING SQLPARAMETERS
                //CREATING NEW SQLPARAMETES AS AN ARRAY TO MANIPULATE AND ISERT DATA IN TO
                //MY GALLERIES DATABASE
                SqlParameter[] myparams = new SqlParameter[6];
                myparams[0] = new SqlParameter("@name", GalleryName_New);
                myparams[1] = new SqlParameter("@description", GalleryDescription_New);
                myparams[2] = new SqlParameter("@date", DateTime.Now);
                myparams[3] = new SqlParameter("@category", GalleryCategory);
                myparams[4] = new SqlParameter("@path", ImagePath);
                myparams[5] = new SqlParameter("@set", SetPic);

                //THIS WILL EXECUTE THE QUERY AND INSERT DATA TO DATABASE
                db.Database.ExecuteSqlCommand(query, myparams);

                //ONCE THE QUERY IS RUN THIS TILL REDEIRECT TO LIST.CSHTML WHERE IT SHOWS
                //LIST OR VIEWS OF CREATED GALLERIES
                return RedirectToAction("List");
            }
            //GO TO GALLY VIEWS LIST.CSHTML
            return View(gallery);
        }

        public ActionResult Edit(int id)
        {
            GalleryEdit galleryeditview = new GalleryEdit
            {

                //SELECTING CREATED GALLERIES FROM THE DATABASE
                gallery = db.Galleries.Find(id)
            };
            return View(galleryeditview);
        }

        [HttpPost]
        public ActionResult Edit(int id, string GalleryName_New, string GalleryDescription_New)
        {
            if ((id == null) || (db.Galleries.Find(id) == null))
            {
                return HttpNotFound();
            }
            //QUERY TO UPDATE AND EDIT EXISTING GALLERY
            string query = "update Galleries set GalleryName = @name, GalleryDescription = @description where GalleryID = @id";

            //AGAIN SETTING SQLPARAMETERS TO MANIPULATE AND INSERT DATA TO MY DATABASE
            SqlParameter[] myparams = new SqlParameter[3];
            myparams[0] = new SqlParameter("@name", GalleryName_New);
            myparams[1] = new SqlParameter("@description", GalleryDescription_New);
            myparams[2] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);
            //AFTER THE QUERY IS RUN IT WILL REDIRECT TO SHOW.CSHTML FROM GALLERY VIEWS
            return RedirectToAction("Show/" + id);

        }
        public ActionResult Show(int? id)
        {

            if ((id == null) || (db.Galleries.Find(id) == null))
            {
                return HttpNotFound();

            }

            //ANOTHER SQLPARAMETER 
            string query = "select * from Galleries where GalleryID = @id";

            // THIS TIME IT WILL SELECT A SPECIFIC GALLERY AND DISPLAY THE LIST OF
            // IMAGES ASSOCIATED AND UPLOADED FOR THIS GALLERY

            SqlParameter[] myparams = new SqlParameter[1];
            myparams[0] = new SqlParameter("@id", id);

            Gallery galleryShow = db.Galleries.SqlQuery(query, myparams).FirstOrDefault();

            //THIS WILL DISPLAY A SPECIFIC GALLERY AND THE IMAGES UPLOADED TO THIS GALLERY
            return View(galleryShow);
        }

        public ActionResult Delete(int? id)
        {
            //SINCE I AM DOING ONE TO MANY RELATIONSHIP WHICH IS ONE GALLERY TO MANY IMAGES
            //I HAVE TO DELETE THE FOREIGN KEY OR GALLERYID ASSOCIATED TO MY IMAGES IN MY IMAGES TABLE FIRST BEFORE DELETING THE GALLERY
            //gallery_GallryID IS THE FOREIGN KEY IN IMAGE TABLE OR DATABASE ASSOCIATED TO ONE GALLERY
            string query = "delete from Images where gallery_GalleryID = @id";
            SqlParameter myparams = new SqlParameter("@id", id);

            //RUN AND EXSECUTING THE COMMAND TO DELETED GALLERY_GALLERYID IN IMAGES TABLE/MODEL
            db.Database.ExecuteSqlCommand(query, myparams);

            //FINALLY DELETEING THE ONE GALLERY IN THE GALLRY TABLE
            query = "delete from Galleries where GalleryID = @id";
            myparams = new SqlParameter("@id", id);

            //EXECUTING THE COMMAND TO DELETED A CERTAIN GALLERY
            db.Database.ExecuteSqlCommand(query, myparams);

            //AFTER DELETING THIS WILL TAKE BACK TO LIST VIEW OF THE GALLEIRES
            return RedirectToAction("List");
        }
    }
}

//I TRIED MY BEST TO UNDERSTAND THE FLOW AND HOW THIS ENTIRE THING WORK
// I STILL NEED HELP AND GUIDANCE
// SO I USE YOUR EXAMPLE AS A REFERENCE AND ALSO MICROSOFT DOCUMENTATION AND OTHER TUTORIALS ONLINE
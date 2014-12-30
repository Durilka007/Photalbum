using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using PhotoalbumMvcPL.ViewModels;

namespace PhotoalbumMvcPL.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IPhotoeService photoService;

        public PhotoController(IPhotoeService photoService, IAlbumService albumService)
        {
            this.albumService = albumService;
            this.photoService = photoService;
        }
        public ActionResult Album(int albumid)
        {
            ViewBag.AlbumId = albumid;
            //if (Session["Email"] != null)
            //{
            //    var email = Session["Email"].ToString();
            //    int userid = userService.GetAll().FirstOrDefault(user => user.Email == email).Id;
            //    //  Session["UserId"] = userid.ToString();
            //    if (userId == userid)
            //    {
            //        return RedirectToAction("Me", "Profile");
            //    }
            //}
            return
                View(photoService.GetAll().Where(photo => photo.AlbumId == albumid) // IEnumerable<AlbumEntity> GetAll();
                    .Select(photo => new PhotoViewModel()
                    {
                        Id = photo.Id,
                        AlbumId = photo.AlbumId,
                        Like = photo.Like,
                        Description = photo.Description,
                        ImagePhotoe = photo. ImagePhotoe,
                        ImagePhotoMimeType = photo.ImagePhotoMimeType,
                        AddTime = photo.AddTime 
                    }));
        }

        [HttpPost]
        public ActionResult Album( HttpPostedFileBase image)
        {
            
            if (image != null)
            {
                byte[] photo = new byte[image.ContentLength];
                image.InputStream.Read(photo, 0, image.ContentLength);


                 photoService.Create(new PhotoeEntity()
                    {

                AlbumId = ViewBag.AlbumId,
                Like = 0,
                Description ="...",
                ImagePhotoe = photo,
                ImagePhotoMimeType = image.ContentType,
                AddTime = DateTime.Now 
                
                    });
            }

            return RedirectToAction("Album", "Photo", new { albumId = ViewBag.AlbumId });
        }




        public FileContentResult GetImage(int photoId)
        {
            var photo = photoService.GetAll().Select(f => f.ToDalPhotoe()).FirstOrDefault(f => f.Id == photoId);//DalPhotoe
            if (photo != null)
            {
                return File(photo.ImagePhotoe, photo.ImagePhotoMimeType);
            }
            else
            {
                return null;
            }
        }

       
    }
}

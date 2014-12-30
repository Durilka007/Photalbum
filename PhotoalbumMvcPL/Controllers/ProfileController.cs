using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Routing;
using System.Web.WebPages;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using ORM;
using PhotoalbumMvcPL.ViewModels;

namespace PhotoalbumMvcPL.Controllers
{
    public class ProfileController : Controller
    {
        //private readonly int userId;
        private readonly IAlbumService albumService;
        private readonly IUserService userService;

        public ProfileController(IAlbumService albumService, IUserService userService)
        {
            this.albumService = albumService;
            this.userService = userService;
        }

        public ActionResult Albums(int userId)
        {
            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                int userid = userService.GetAll().FirstOrDefault(user => user.Email == email).Id;
                //  Session["UserId"] = userid.ToString();
                if (userId == userid)
                {
                    return RedirectToAction("Me", "Profile");
                }
            }
            return
                View(albumService.GetAll().Where(album => album.UserId == userId) // IEnumerable<AlbumEntity> GetAll();
                    .Select(album => new AlbumViewModel()
                    {
                        Id = album.Id,
                        AlbumName = album.AlbumName,
                        Description = album.Description,
                        UserId = album.UserId,
                        UserName = userService.GetById(album.UserId).UserName,
                        CreationTime = album.CreationTime
                    }));

        }
        [HttpGet]
        public ActionResult New()
        {
            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                int userId = userService.GetAll().FirstOrDefault(user => user.Email == email).Id;
                return View();
            }
            //albumService.Create(new AlbumEntity());
            //return RedirectToAction("Albums", "Profile", new { userId = user.Id });
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult New(AlbumViewModel  viewModel)
        {
            if (Session["Email"] != null)
            {
                if (ModelState.IsValid)
                {
                    var email = Session["Email"].ToString();
                    int userId = userService.GetAll().FirstOrDefault(user => user.Email == email).Id;
                    if (viewModel.AlbumName.IsEmpty())///////
                    {/////
                        viewModel.AlbumName = "New album";/////
                    }//////
                    albumService.Create(new AlbumEntity()
                    {
                        AlbumName = viewModel.AlbumName,
                        Description = viewModel.Description,
                        UserId = userId,
                        CreationTime = DateTime.Now
                    });

                    
                }
                return RedirectToAction("Me", "Profile");
            }
            return RedirectToAction("Login", "Account");

        }

        public ActionResult Delete(int albumId)
        {

            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                int userid = userService.GetAll().FirstOrDefault(user => user.Email == email).Id;
                var album = albumService.GetAll().FirstOrDefault(u => u.Id == albumId);

                if (album.UserId == userid)
                {
                    if (album != null)
                    {
                        albumService.Delete(album);

                    }
                }
            }

            return RedirectToAction("Me", "Profile");
            
        }

        public ActionResult Me()
        {
            if (Session["Email"] != null)
            {
                var email = Session["Email"].ToString();
                int userId = userService.GetAll().FirstOrDefault(user => user.Email == email).Id;
                Session["UserId"] = userId;

                return
                    View(albumService.GetAll()
                        .Where(album => album.UserId == (int) (Session["UserId"])) // IEnumerable<AlbumEntity> GetAll();
                        .Select(album => new AlbumViewModel()
                        {
                            Id = album.Id,
                            AlbumName = album.AlbumName,
                            Description = album.Description,
                            UserId = album.UserId,
                            UserName = userService.GetById(album.UserId).UserName,
                            CreationTime = album.CreationTime
                        }));
            }
            else return RedirectToAction("Login", "Account");
        }
    }
}

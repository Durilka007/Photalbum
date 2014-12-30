using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using Microsoft.Ajax.Utilities;
using ORM;
using PhotoalbumMvcPL.Infrastructure;
using PhotoalbumMvcPL.Providers;
using PhotoalbumMvcPL.ViewModels;


namespace PhotoalbumMvcPL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public AccountController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        public ActionResult Index()
        {
           // var l = userService.GetAll();//IEnumerable<UserEntity> GetAll();
            return View(userService.GetAll()// IEnumerable<UserEntity> GetAll();
                .Select(user => new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreationDate = user.CreationTime,
                    Role= roleService.GetById(user.RoleId).RoleName,
                    //Role = (user.RoleId==2)?"admin":"user",
                    UserPhotoe = user.UserPhotoe,
                    UserPhotoMimeType= user.UserPhotoMimeType,
                    LastUpdateTime= user.LastUpdateTime
                }));

            //var model = from u in repository.GetAll()//IEnumerable<DalUser> GetAll()
            //            select new UserViewModel
            //            {
            //                Email = u.Email,
            //                CreationDate = u.CreationTime,
            //                Role = u.Role.RoleName
            //            };
            //return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //session
                        Session["Email"] = viewModel.Email;
                        return RedirectToAction("Me", "Profile");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }

            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
           // Session["Email"] = null;
            Session.Clear();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel, HttpPostedFileBase image)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
                return View(viewModel);
            }

            var anyUser = userService.GetAll().Any(u => u.Email.Contains(viewModel.Email));
            if (anyUser)
            {
                ModelState.AddModelError("Email", "Пользователь с таким адресом уже зарегистрирован");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    viewModel.UserPhotoMimeType = image.ContentType;
                    viewModel.UserPhotoe = new byte[image.ContentLength];
                    image.InputStream.Read(viewModel.UserPhotoe, 0, image.ContentLength);
                }
                //session
                Session["Email"] = viewModel.Email;
                MembershipUser membershipUser = ((CustomMembershipProvider)Membership.Provider).CreateUser(viewModel.UserName, viewModel.Password, viewModel.Email, viewModel.UserPhotoMimeType, viewModel.UserPhotoe);
              
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Me", "Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Ошибка при регистрации");
                }
            }

            return View(viewModel);
        }

        //В сессии создаем случайное число от 1111 до 9999.
        //Создаем в ci объект CatchaImage
        //Очищаем поток вывода
        //Задаем header для mime-типа этого http-ответа будет "image/jpeg" т.е. картинка формата jpeg.
        //Сохраняем bitmap в выходной поток с форматом ImageFormat.Jpeg
        //Освобождаем ресурсы Bitmap
        //Возвращаем null, так как основная информация уже передана в поток вывода
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }
        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }


        public FileContentResult GetImage(int userId)
        {
            var user = userService.GetAll().Select(u => u.ToDalUser()).FirstOrDefault(u => u.Id == userId);//DalUser
            if (user != null)
            {
                return File(user.UserPhotoe, user.UserPhotoMimeType);
            }
            else
            {
                return null;
            }
        }
      
        public ActionResult Delete(int userId)
        {
            var user = userService.GetAll().FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                userService.Delete(user);
            }

            return RedirectToAction("Index", "Account");
           
        }
    }
}

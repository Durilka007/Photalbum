using System.Linq;
using System.Web.Mvc;
using BLL.Interfaces.Entities;
using BLL.Interfaces.Services;
using Microsoft.Ajax.Utilities;
//using PhotoalbumMvcPL.Models;

namespace PhotoalbumMvcPL.Controllers
{
    public class HomeController : Controller
    {

        //
        // GET: /Home/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public void Index()
        {
            // HttpContext.Profile["FirstName"] = "Guest";
            //HttpContext.Profile["LastName"] = "Иванов";
            //HttpContext.Profile.SetPropertyValue("Age", 23);

            //Response.Write(HttpContext.Profile.GetPropertyValue("FirstName"));
            // Response.Write(HttpContext.Profile.GetPropertyValue("LastName"));
            //  string str = " Вы вошли с правами ";
            if (HttpContext.User.Identity.Name.IsNullOrWhiteSpace() == false)
            {
                Response.Write("Ваш логин: " + HttpContext.User.Identity.Name);
            }
            Response.Write(" Вы вошли с правами ");
            if (HttpContext.User.IsInRole("admin") == true)
            {
                Response.Write("администратора");
            }
            else if (HttpContext.User.IsInRole("user") == true)
            {
                Response.Write("пользователя");
            }
            else Response.Write("гостя");
        }
    

        [Authorize(Roles = "admin")]
        public string ViewRole()
        {
            return "Ваша роль: Администратор";
        }
    }
}

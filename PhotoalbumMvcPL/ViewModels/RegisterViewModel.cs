using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PhotoalbumMvcPL.ViewModels
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Введите имя пользователя")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        public string UserName { get; set; }

        [Display(Name = "Введите email")]
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Пароль должен содержать по крайней мере {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли должны совпадать.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Captcha { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreationTime { get; set; }

      //загрузка изображений http://smarly.net/pro-asp-net-mvc-4/introducing-asp-net-mvc-4/sportsstore-security-finishing-touches/image-uploads
        [Display(Name = "Аватар пользователя")]
        public byte[] UserPhotoe { get; set; }//Свойство не будет видно в коде страницы по умолчанию типа без атрибутов!!!
        //[StringLength(50)]

        [HiddenInput(DisplayValue = false)]//Свойство не будет видно ни на форме, ни в коде страницы!!! 
        public string UserPhotoMimeType { get; set; }
        //public virtual ICollection<Album> Albums { get; set; }
        //public virtual Role Role { get; set; }




    }
}
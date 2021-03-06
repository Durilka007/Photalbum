﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PhotoalbumMvcPL.ViewModels
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Роль в системе")]
        public string Role { get; set; }

        [Display(Name = "Аватар")]
        public byte[] UserPhotoe { get; set; }

        [HiddenInput(DisplayValue = false)]//Свойство не будет видно ни на форме, ни в коде страницы!!! 
        public string UserPhotoMimeType { get; set; }

        [Display(Name = "Время последней активности в системе")]
        public DateTime? LastUpdateTime { get; set; }





       
       
        //public DateTime? LastUpdateTime { get; set; }
    }
}
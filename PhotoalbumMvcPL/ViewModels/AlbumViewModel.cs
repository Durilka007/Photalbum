using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoalbumMvcPL.ViewModels
{
    public class AlbumViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name = "Название альбома")]
        public string AlbumName { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime? CreationTime { get; set; }
    }
}
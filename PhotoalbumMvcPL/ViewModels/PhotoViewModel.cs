using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoalbumMvcPL.ViewModels
{
    public class PhotoViewModel
    {
        [ScaffoldColumn(false)]

        public int Id { get; set; }

        [ScaffoldColumn(false)]

        public int AlbumId { get; set; }
        public int Like { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
         [Display(Name = "Фото")]
        public byte[] ImagePhotoe { get; set; }
        public string ImagePhotoMimeType { get; set; }
        [Display(Name = "Время добавления")]
        public DateTime? AddTime { get; set; }
    }
}
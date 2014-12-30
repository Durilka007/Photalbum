﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalAlbum : IEntity
    {
        public int Id { get; set; }
        public string AlbumName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}

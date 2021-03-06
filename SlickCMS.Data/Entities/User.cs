﻿using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public partial class User : IBaseEntity
    {
        public int UserId { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public string Biography { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Active { get; set; }
        public int LoginFails { get; set; }
    }
}

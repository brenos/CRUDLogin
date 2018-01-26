using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace {0}.Models
{
    public class RoleModel
    {
        [Required]
        [Display(Name = "Regra")]
        public string Regra { get; set; }
    }
}
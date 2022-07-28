using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEshop2.Models
{
    [MetadataType(typeof(RolesMetaData))]
    public class RolesMetaData
    {
        [Key]
        public int RoleID { get; set; }
        [Display(Name = " عنوان نمایشی نقش")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]

        public string RoleTitle { get; set; }
        [Display(Name = "عنوان سیستمی نقش")]
        [Required(ErrorMessage = "لطفا{0} را وارد کنید")]
        public string RoleName { get; set; }

    }
}
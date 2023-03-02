using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTQ_Solution.Areas.Admin.Data
{
    public class RigisterModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Hãy nhập Email")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
        [RegularExpression(@"^.{10,30}$", ErrorMessage = "{0} chỉ từ 10 đến 30 ký tự.")]
        public string Email { get; set; }
        [RegularExpression(@"^.{8,20}$", ErrorMessage = "{0} chỉ từ 8 đến 20 ký tự.")]
        [Required(ErrorMessage = "Hãy nhập Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Hãy nhập nhập comfirmPasword ")]
        public string ComfimPassword { get; set; }
        [Required(ErrorMessage = "không được bỏ trống UserName")]
        public string Username { get; set; }
        public bool Role { get; set; }
    }
}
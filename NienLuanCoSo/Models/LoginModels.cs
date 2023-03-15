using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NienLuanCoSo.Models
{
    public class LoginModels
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nhập tài khoản!")]
        public string Username { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nhập mật khẩu!")]
        public string Password { set; get; }
    }
}
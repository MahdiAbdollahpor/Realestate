using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class UserInfoForUserPanelViewModel
    {
        [ValidateNever]
        public int UserId { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string DispalyName { get; set; }
        [ValidateNever]
        public string RegisterTime { get; set; }
        [ValidateNever]
        public string PhoneNumber { get; set; }
    }
}

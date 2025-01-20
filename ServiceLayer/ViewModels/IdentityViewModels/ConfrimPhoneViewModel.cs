using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.IdentityViewModels
{
    public class ConfrimPhoneViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "کد تایید ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0}را وارد نمایید")]
        public string code { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Store.enumProperty
{
    public enum RequestStatus
    {
        [Display(Name = "در انتظار تایید")]
        Pending,

        [Display(Name = "تایید شده")]
        Approved,

        [Display(Name = "رد شده")]
        Rejected 
    }
}

using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Store.enumProperty
{
    public enum TransactionType
    {
        [Display(Name = "اجاره")]
        Rent,

        [Display(Name = "فروش")]
        Sale 
    }
}

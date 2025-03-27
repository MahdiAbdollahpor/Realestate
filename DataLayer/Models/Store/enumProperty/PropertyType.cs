using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models.Store.enumProperty
{
    public enum PropertyType
    {
        [Display(Name = "خانه حیاط دار")]
        HouseWithYard,

        [Display(Name = "آپارتمان")]
        Apartment,

        [Display(Name = "مغازه")]
        Shop,

        [Display(Name = "دفتر")]
        Office,

        [Display(Name = "ویلا")]
        Villa
    }
}

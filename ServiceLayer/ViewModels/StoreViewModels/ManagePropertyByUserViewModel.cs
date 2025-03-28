﻿using DataLayer.Models.Store.enumProperty;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels.StoreViewModels
{
    public class ManagePropertyByUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نوع ملک")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public PropertyType Type { get; set; }

        [Display(Name = "نوع معامله")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public TransactionType TransactionType { get; set; }

        [Display(Name = "آدرس ملک")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Address { get; set; }

        [Display(Name = "قیمت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public decimal Price { get; set; }

        [Display(Name = "توضیحات کامل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Description { get; set; }

        [Display(Name = "متراژ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Meterage { get; set; } // متراژ

        [Display(Name = "طبقه چندم ؟")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Floor { get; set; } // طبقه چندم ؟

        [Display(Name = "چند اتاقه ؟")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Room { get; set; } //  چند اتاقه ؟ 

        [Display(Name = "پارکینگ ؟")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public bool Parking { get; set; } // پارکینگ ؟

        [Display(Name = "انباری ؟")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public bool warehouse { get; set; }  // انباری ؟

        [Display(Name = "آسانسور ؟")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public bool elevator { get; set; } // آسانسور ؟ 

        [Display(Name = "حمام و دستشویی ؟")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا {0} را وارد نمایید")]
        public bool Bathroom { get; set; } // حمام و دستشویی ؟

        [Display(Name = "تصویر اصلی")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب نمایید")]
        public IFormFile IndexImage1 { get; set; }

        [Display(Name = "تصویر دوم")]
        public IFormFile IndexImage2 { get; set; }

        [Display(Name = "تصویر سوم")]
        public IFormFile IndexImage3 { get; set; }
        public int UserId { get; set; }
    }
}

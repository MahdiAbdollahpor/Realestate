using DataLayer.Models.Identity;
using DataLayer.Models.Store.enumProperty;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Store
{
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }

        public PropertyType Type { get; set; } // نوع ملک (خانه حیاط دار، آپارتمان، مغازه، دفتر، ویلا، زمین خالی)

        public TransactionType TransactionType { get; set; } // نوع معامله (اجاره یا فروش)

        public string Address { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Meterage { get; set; } // متراژ

        public string Floor { get; set; } // طبقه چندم ؟

        public string Room { get; set; } //  چند اتاقه ؟ 

        public bool Parking { get; set; } // پارکینگ ؟

        public bool warehouse { get; set; }  // انباری ؟

        public bool elevator { get; set; } // آسانسور ؟ 

        public bool Bathroom { get; set; } // حمام و دستشویی ؟

        public string IndexImage1 { get; set; }

        public string IndexImage2 { get; set; }

        public string IndexImage3 { get; set; }

        public RequestStatus Status { get; set; } // وضعیت درخواست (تایید شده، در انتظار تایید، رد شده)

        #region rel


        public int UserId { get; set; } // کلید خارجی برای کاربر
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } // ناوبری به کاربر

        #endregion
    }
}

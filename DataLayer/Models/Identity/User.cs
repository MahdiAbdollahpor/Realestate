using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Identity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public bool ConfrimPhoneNumber { get; set; }
        public string ConfrimCode { get; set; }
        public DateTime ConfrimCodeCreateDate { get; set; }
        public DateTime RegisterTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }


        #region rel

        public IEnumerable<UserRole> UserRoles { get; set; }

        
        #endregion
    }
}

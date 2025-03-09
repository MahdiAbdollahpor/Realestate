using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Store.enumProperty
{
    public enum RequestStatus
    {
        Pending, // در انتظار تایید
        Approved, // تایید شده
        Rejected // رد شده
    }
}

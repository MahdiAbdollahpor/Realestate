using ServiceLayer.ViewModels.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IIdentityService
    {
        int RegisterByPhoneNumber(RegisterViewModel model);
        int IsExistPhoneNumber(string PhoneNumber);
        bool IsPhoneNumberExist(string PhoneNumber);
        int ConfrimPhoneNumber(string phoneNumber, string code);
        int GetUserIdByPhoneNumber(string phoneNumber);
    }
}

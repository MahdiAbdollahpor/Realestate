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
        int GetUserStatusForLoginByPhoneNumber(string phoneNumber, string password);
        int GetPhoneNumberStatusForForgotPass(string PhoneNumber);
        int ResetPasswordByMobile(RsetPasswordViewModel model);
        string GetDisplayNameByPhoneNumber(string phone);
        bool CheckPermission(int permissionId, string phoneNumber);
        bool UpdateUserInfoFromUserPanel(UserInfoForUserPanelViewModel model);
        bool AddNewPassword(NewPasswordViewModel model);
        public UserPanelSidebarViewModels GetUserPanelSidebar(string phoneNumber);
        UserInfoForUserPanelViewModel GetUserInfoForUserPanel(string phoneNumber);
    }
}

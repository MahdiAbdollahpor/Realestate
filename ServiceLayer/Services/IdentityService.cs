using DataLayer.Context;
using DataLayer.Models.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.PublicClasses;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.IdentityViewModels;

namespace ServiceLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext _db;
        private readonly ISmsSender _smsSender;


        public IdentityService(ApplicationDbContext db, ISmsSender smsSender)
        {
            _db = db;
            _smsSender = smsSender;
        }

        //-1 => ثبت نام با شکست مواجه شد
        // 1 => ثبت نام با موفقیت انجام شد
        // -100 => کاربر قبلا ثبت نام کرده
        // -50 => بعد از 3 دقیقه باید تلاش کنبد
        public int RegisterByPhoneNumber(RegisterViewModel model)
        {
            int StatusPhoneNumber = IsExistPhoneNumber(model.PhoneNumber);
            if (StatusPhoneNumber == 1)
            {
                User user = new User
                {
                    ConfrimCode = GenerateVerifyCode(),
                    ConfrimCodeCreateDate = DateTime.Now,
                    ConfrimPhoneNumber = false,
                    DisplayName = model.DisplayName,
                    IsDeleted = false,
                    Password = PasswordHelper.EncodePasswordMd5(model.Password),
                    PhoneNumber = model.PhoneNumber,
                };

                //todo => send sms
                //_smsSender.SendSms(1, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

                _db.Users.Add(user);
                _db.SaveChanges();
                return 1;

            }
            if (StatusPhoneNumber == -100)
            {
                return -100;
            }
            if (StatusPhoneNumber == -50)
            {

                var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);
                if (user.ConfrimCodeCreateDate.AddSeconds(20) > DateTime.Now)
                {
                    return -50;
                }
                user.DisplayName = model.DisplayName;
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);
               

                //todo => send sms

               // _smsSender.SendSms(1, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

                _db.Update(user);
                _db.SaveChanges();
                return 1;
            }

            return -1;
        }

        //1 => شماره موبایل وجود ندارد
        //-100 => قبلا ثبت نام اتجام شده
        // -50 => شماره موبایل تایید نشده
        // -1 => خطای دیتابیس
        public int IsExistPhoneNumber(string PhoneNumber)
        {
            var res = _db.Users.FirstOrDefault(u => u.PhoneNumber == PhoneNumber);
            if (res == null)
            {
                return 1;
            }
            if (res.ConfrimPhoneNumber == true)
            {
                return -100;
            }
            if (res.ConfrimPhoneNumber == false)
            {
                return -50;
            }
            return -1;
        }

        public string GenerateVerifyCode()
        {
            Random random = new Random();
            return random.Next(12345, 99999).ToString();
        }

        public bool IsPhoneNumberExist(string phoneNumber)
        {
            //var res = _db.Users.FirstOrDefault(u => u.PhoneNumber == PhoneNumber);
            //if (res == null)
            //{
            //    return false;
            //}
            //return true;
            return _db.Users.Any(u => u.PhoneNumber == phoneNumber);
        }

        //-100 => کاربر یافت نشد
        // -50 => کد تایید منقضی شده
        // -200 => کد تایید تطابق ندارد
        public int ConfrimPhoneNumber(string phoneNumber, string code)
        {
            var user = _db.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

            if (user == null)
            {
                return -100;
            }
            if (user.ConfrimCodeCreateDate.AddMinutes(15) < DateTime.Now)
            {
                return -50;
            }
            if (user.ConfrimCode != code)
            {
                return -200;
            }
            else
            {
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimPhoneNumber = true;

                _db.Users.Update(user);
                _db.SaveChanges();
                return 1;
            }

            
        }


        public int GetUserIdByPhoneNumber(string phoneNumber)
        {
            var res = _db.Users.FirstOrDefault(u =>u.PhoneNumber == phoneNumber);
            
            return res.UserId;
            
        }

        //-100 => کاربر وجود ندارد
        //-50 => پسورد اشتباه است
        // -150 => حساب کاربری فعال نیست
        // -200 =>کاربر حذف شده است
        public int GetUserStatusForLoginByPhoneNumber(string phoneNumber ,string password)
        {
            var res = _db.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

            if(res == null)
            {
                return -100;
            }
            if(res.Password!= PasswordHelper.EncodePasswordMd5(password))
            {
                return -50;
            }
            if(res.ConfrimPhoneNumber == false)
            {
                return -150;
            }
            if(res.IsDeleted == true)
            {
                return -200;
            }

            return -1;
        }

        public int GetPhoneNumberStatusForForgotPass(string PhoneNumber)
        {


            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);

            if (user == null)
            {
                return -100;

            }
            if (user.IsDeleted == true)
            {
                return -50;
            }
            if (user.ConfrimPhoneNumber == false)
            {
                return -200;
            }

            else
            {
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;

                bool res = _smsSender.SendSms(2, user.PhoneNumber, user.DisplayName, user.ConfrimCode);


                if (res == true)
                {
                    _db.Users.Update(user);
                _db.SaveChanges();
                return 1;
                }
            }
            return -1;

        }

        public int ResetPasswordByMobile(RsetPasswordViewModel model)
        {
            var user = _db.Users.FirstOrDefault(x => x.PhoneNumber == model.PhoneNumber);

            if (user == null)
            {
                return -100;
            }
            if (user.ConfrimCodeCreateDate.AddMinutes(15) < DateTime.Now)
            {
                return -50;
            }
            if (user.ConfrimCode != model.Code)
            {
                return -200;
            }
            else
            {
                user.ConfrimCode = GenerateVerifyCode();
                user.ConfrimCodeCreateDate = DateTime.Now;
                user.Password = PasswordHelper.EncodePasswordMd5(model.Password);

                _db.Users.Update(user);
                _db.SaveChanges();
                return 1;
            }

            return -1;
        }

        public string GetDisplayNameByPhoneNumber(string phone)
        {
            return _db.Users.FirstOrDefault(x => x.PhoneNumber == phone).DisplayName;
        }

        public bool CheckPermission(int permissionId, string phoneNumber)
        {
            int userId = _db.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber).UserId;

            List<int> roleIds = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();

            bool flag = false;

            if (!roleIds.Any())
            {
                flag = false;
            }
            else
            {
                foreach (int roleId in roleIds)
                {
                    foreach (var _rolePermission in _db.RolePermissions.Where(x => x.RoleId == roleId).ToList())
                    {
                        if (_rolePermission.PermissionId == permissionId)
                        {
                            flag = true;
                        }
                    }
                }
            }

            return flag;

        }

        public UserInfoForUserPanelViewModel GetUserInfoForUserPanel(string phoneNumber)
        {
            return _db.Users.Where(x => x.PhoneNumber == phoneNumber).Select(x => new UserInfoForUserPanelViewModel
            {
                DispalyName = x.DisplayName,
                PhoneNumber = x.PhoneNumber,
                RegisterTime = MyDateTime.GetShamsiDateFromGregorian(x.RegisterTime, false),
                UserId = x.UserId
            }).FirstOrDefault();
        }

        


        }
    }

using DataLayer.Context;
using DataLayer.Models.Identity;
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
                _smsSender.SendSms(1, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

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

                _smsSender.SendSms(1, user.PhoneNumber, user.DisplayName, user.ConfrimCode);

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
    }
}

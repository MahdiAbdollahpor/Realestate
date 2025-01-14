namespace ServiceLayer.PublicClasses
{
    public interface ISmsSender
    {
        bool SendSms(int type, string PhoneNumber, string DisplayName, string verificationCode);
    }
    public class SmsSender : ISmsSender
    {
        public bool SendSms(int type, string PhoneNumber, string DisplayName, string verificationCode)
        {
            string apiKey = "";
            string patternSms = "";
            string displayName = DisplayName.Replace(" ", "_");
            string fromNumber = "********";
            string url = "";


            if (type == 1)//register
            {
                patternSms = "75r5fo7l7u";
                url = $"http://ippanel.com:8080/?apikey={apiKey}&pid={patternSms}&fnum={fromNumber}&tnum={PhoneNumber}&p1=name&p2=verification-code&v1={displayName}&v2={verificationCode}";

            }
            if (type == 2)//forgot pass
            {
                patternSms = "cj0jdm47kltfy8f";
                url = $"http://ippanel.com:8080/?apikey={apiKey}&pid={patternSms}&fnum={fromNumber}&tnum={PhoneNumber}&p1=code&v1={verificationCode}";
            }


            HttpClient httpClient = new HttpClient();
            var httpResponse = httpClient.GetAsync(url);
            if (httpResponse.IsCompleted == true)
            {
                return true;
            }
            return false;
        }
    }
}

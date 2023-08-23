using System;
using System.Configuration;
using System.Net;

namespace AfluexSchool.Models
{
    static public class BLSMS
    {
        public static void SendSMS2(string User, string password, string senderid, string Mobile_Number, string Message)
        {
            try
            {
                string SMSAPI = ConfigurationSettings.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile_Number);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);


                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }
        
    }
}
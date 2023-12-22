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

        
        /////////////////////////////////////////////////////////////////
        
        static public string DemoRequestOTP(string Name, string OTP)
        {
            string Message = ConfigurationSettings.AppSettings["DemoRequestOTP"].ToString();

            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[OTP]", OTP);

            return Message;
        }
        
        static public string ForgetPassword(string Name, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();

            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }
        
        static public void SendSMS(string Mobile, string Message, string TempId)
        {
            try
            {
                string SMSAPI = ConfigurationSettings.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);
                SMSAPI = SMSAPI.Replace("[TempId]", TempId);
                SMSAPI = SMSAPI.Replace("[Date]", DateTime.Now.ToString());
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }

        /////////////////////////////////////////////////////////////////

    }
}
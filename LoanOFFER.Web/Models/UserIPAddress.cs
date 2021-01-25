using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanOFFER.Web.Models
{
    public class UserIPAddress
    {
        public static string GetIP(HttpRequest request)
        {
            string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = request.ServerVariables["REMOTE_ADDR"];
            }

            return ipAddress;
        }
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetCurrentComputerName(string[] hostname)
        {
            try
            {
                ////  String hostName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                //String hostName = Dns.GetHostEntry(Request)
                return "hostname";
            }
            catch (Exception ex)
            {

                var error = ex.Message;
            }
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Client.Util {
    public class Constants {
        public static String ldapServerName = ConfigurationManager.AppSettings.Get("LDAP_SERVER_NAME");
        public static String ldapUserName = ConfigurationManager.AppSettings.Get("LDAP_USERNAME");
        public static String ldapPassword = ConfigurationManager.AppSettings.Get("LDAP_PASSWORD");

        public static string AUTHORIZATION_COOKIE_NAME = "AUTHORIZATION_COOKIE";
    }
}
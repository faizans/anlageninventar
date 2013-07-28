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

        public static string EXCEL_TEMPLATE_NAME = "ReportTemplate.xls";
        public static string EXCEL_EXPORT_NAME = "ExcelReport.xls";
        public static string EXCEL_TEMPLATE_FOLDER = "~/ExcelTemplates/";
    }
}
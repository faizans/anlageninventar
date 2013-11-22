using Data.Enum;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Client.Util {
    public class Constants {
        public static String ldapServerName = ConfigurationManager.AppSettings.Get("LDAP_SERVER_NAME");
        public static String ldapUserName = ConfigurationManager.AppSettings.Get("LDAP_USERNAME");
        public static String ldapPassword = ConfigurationManager.AppSettings.Get("LDAP_PASSWORD");

        public static string AUTHORIZATION_COOKIE_NAME = "AUTHORIZATION_COOKIE";
        public static string AUTHORIZATION_WINDOWS_LOGIN = "~/SilentLogin.aspx";
        public static string AUTHORIZATION_FORMS_LOGIN = "~/Login.aspx";

        public static string DEFAULT_PAGE = "~/default.aspx";

        public static string EXCEL_TEMPLATE_NAME = "ReportTemplate.xls";
        public static string EXCEL_EXPORT_NAME = "ExcelReport.xls";
        public static string EXCEL_TEMPLATE_FOLDER = "~/ExcelTemplates/";

        public static string TELERIK_TEMPLATE_FOLDER = "~/ExcelTemplates/";

        public static List<String> TELERIK_REPORT_TEMPLATES {
            get {
                return Enum.GetValues(typeof(TelerikReports)).Cast<TelerikReports>().Select(e => e.ToString()).ToList();
            }
        }

        public static NumberFormatInfo NUMBER_GROUP_FORMAT = new NumberFormatInfo() {
            NumberGroupSeparator = "'"
        };

        public static string NUMBER_FORMAT = "##,##0.00";
    }
}
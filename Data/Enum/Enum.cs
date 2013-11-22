using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enum
{
    public enum UserRole
    {
        Administrator,
        User
    }

    public enum SessionName {
        LoggedUser,
        ExportItems,
        ReportYear,
        SelectedTemplate,
        SelectedTelerikTemplate,
        InOutYear

    }

    public enum DownloadType {
        Template
    }

    public enum ReportType {
        Categorized
    }

    public enum ReportCategories {
        Room,
        Building,
        Responsible,
        Group
    }

    public enum TelerikReports { 
        Standart,
        RaumCheckliste,
        RaumGruppierung,
        KategorieGruppierung
    }
}


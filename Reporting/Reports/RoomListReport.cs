namespace Reporting.Reports {
    using Data.Model.Diagram;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using System.Linq;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RoomGroupingReport.
    /// </summary>
    public partial class RoomListReport : Telerik.Reporting.Report {
        public RoomListReport() {
            InitializeComponent();

            this.DataSource = Room.GetAll().ToList();
        }


        public RoomListReport(string title, string year) {
            InitializeComponent();

            this.txtTitle.Value = title;
            this.txtYear.Value = year;
            this.DataSource = Room.GetAll().ToList();
        }
    }
}
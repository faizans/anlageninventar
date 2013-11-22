namespace Reporting.Reports {
    using Data.Model.Diagram;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;

    /// <summary>
    /// Summary description for RoomGroupingReport.
    /// </summary>
    public partial class RoomGroupList : Telerik.Reporting.Report {
        public RoomGroupList() {
            InitializeComponent();
        }

        public RoomGroupList(List<Article> data) {
            InitializeComponent();
            data.ForEach(a => a.UseStoredValues = true);
            this.DataSource = data;
        }

        public RoomGroupList(List<Article> data, string year) {
            InitializeComponent();
            data.ForEach(a => a.UseStoredValues = true);
            this.DataSource = data;

            this.txtYear.Value = year;
        }

        public RoomGroupList(List<Article> data, string title, string year) {
            InitializeComponent();
            data.ForEach(a => a.UseStoredValues = true);
            this.DataSource = data;

            this.txtTitle.Value = title;
            this.txtYear.Value = year;
        }
    }
}
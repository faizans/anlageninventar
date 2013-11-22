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
    public partial class RoomCheckList : Telerik.Reporting.Report {
        public RoomCheckList() {
            InitializeComponent();
        }

        public RoomCheckList(List<Article> data) {
            InitializeComponent();
            data.ForEach(a => a.UseStoredValues = true);
            this.DataSource = data;
        }
    }
}
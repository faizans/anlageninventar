namespace Reporting.Reports {
    partial class KategorieGroupList
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KategorieGroupList));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            this.roomFloorBuildingGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.txtRoomTotal = new Telerik.Reporting.TextBox();
            this.roomFloorBuildingGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.valueCaptionTextBox = new Telerik.Reporting.TextBox();
            this.barcodeCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.txtTitle = new Telerik.Reporting.TextBox();
            this.txtYear = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.valueDataTextBox = new Telerik.Reporting.TextBox();
            this.barcodeDataTextBox = new Telerik.Reporting.TextBox();
            this.nameDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // roomFloorBuildingGroupFooterSection
            // 
            this.roomFloorBuildingGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0082694292068481D);
            this.roomFloorBuildingGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox5,
            this.textBox27,
            this.textBox24,
            this.textBox13,
            this.txtRoomTotal});
            this.roomFloorBuildingGroupFooterSection.Name = "roomFloorBuildingGroupFooterSection";
            this.roomFloorBuildingGroupFooterSection.PageBreak = Telerik.Reporting.PageBreak.After;
            this.roomFloorBuildingGroupFooterSection.Style.Visible = true;
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(464.8267822265625D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "Kategorietotal";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = true;
            this.textBox27.Format = "{0:N2}";
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.008125305175781D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox27.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            this.textBox27.Style.Font.Bold = true;
            this.textBox27.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox27.StyleName = "Data";
            this.textBox27.Value = "=Sum(Fields.CumulatedDepreciation)";
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = true;
            this.textBox24.Format = "{0:N2}";
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.706250190734863D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox24.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            this.textBox24.Style.Font.Bold = true;
            this.textBox24.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox24.StyleName = "Data";
            this.textBox24.Value = "=Sum(Fields.AverageDepreciation)";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Format = "{0:N2}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.309999465942383D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox13.StyleName = "Data";
            this.textBox13.Value = "=Sum(Fields.DepreciationValue)";
            // 
            // txtRoomTotal
            // 
            this.txtRoomTotal.CanGrow = true;
            this.txtRoomTotal.Format = "{0:N2}";
            this.txtRoomTotal.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.404375076293945D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.txtRoomTotal.Name = "txtRoomTotal";
            this.txtRoomTotal.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.txtRoomTotal.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            this.txtRoomTotal.Style.Font.Bold = true;
            this.txtRoomTotal.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.txtRoomTotal.StyleName = "Data";
            this.txtRoomTotal.Value = "=Sum(Fields.Value)";
            // 
            // roomFloorBuildingGroupHeaderSection
            // 
            this.roomFloorBuildingGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.roomFloorBuildingGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2});
            this.roomFloorBuildingGroupHeaderSection.Name = "roomFloorBuildingGroupHeaderSection";
            this.roomFloorBuildingGroupHeaderSection.PageBreak = Telerik.Reporting.PageBreak.None;
            this.roomFloorBuildingGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(96.267692565917969D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Katgorie";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.8000004291534424D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(518.67193603515625D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox2.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.DepreciationCategory.Name";
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.40000021457672119D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0529166460037231D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.nameCaptionTextBox,
            this.textBox8,
            this.textBox18,
            this.textBox10,
            this.valueCaptionTextBox,
            this.barcodeCaptionTextBox,
            this.textBox20,
            this.textBox4});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // nameCaptionTextBox
            // 
            this.nameCaptionTextBox.CanGrow = true;
            this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.nameCaptionTextBox.Name = "nameCaptionTextBox";
            this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(188.97637939453125D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.nameCaptionTextBox.Style.Font.Bold = true;
            this.nameCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.nameCaptionTextBox.StyleName = "Caption";
            this.nameCaptionTextBox.Value = "Name";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.70625114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox8.Style.Font.Bold = true;
            this.textBox8.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox8.StyleName = "Caption";
            this.textBox8.Value = "Ansch/Jahr";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.711041450500488D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(60D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox18.Style.Font.Bold = true;
            this.textBox18.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox18.StyleName = "Caption";
            this.textBox18.Value = "Datum";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.310001373291016D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox10.StyleName = "Caption";
            this.textBox10.Value = "Ist-Wert";
            // 
            // valueCaptionTextBox
            // 
            this.valueCaptionTextBox.CanGrow = true;
            this.valueCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.404375076293945D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.valueCaptionTextBox.Name = "valueCaptionTextBox";
            this.valueCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.valueCaptionTextBox.Style.Font.Bold = true;
            this.valueCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.valueCaptionTextBox.StyleName = "Caption";
            this.valueCaptionTextBox.Value = "Preis";
            // 
            // barcodeCaptionTextBox
            // 
            this.barcodeCaptionTextBox.CanGrow = true;
            this.barcodeCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9064583778381348D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.barcodeCaptionTextBox.Name = "barcodeCaptionTextBox";
            this.barcodeCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(100.77951049804688D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.barcodeCaptionTextBox.Style.Font.Bold = true;
            this.barcodeCaptionTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.barcodeCaptionTextBox.StyleName = "Caption";
            this.barcodeCaptionTextBox.Value = "Barcode";
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = true;
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.008125305175781D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox20.Style.Font.Bold = true;
            this.textBox20.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox20.StyleName = "Caption";
            this.textBox20.Value = "Kumuliert";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1.7000000476837158D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox11,
            this.pictureBox1,
            this.txtTitle,
            this.txtYear});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.19999909400939941D), Telerik.Reporting.Drawing.Unit.Cm(0.887916624546051D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3999998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox22.Style.Font.Bold = true;
            this.textBox22.Value = "Report:";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.19999909400939941D), Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.0992012023925781D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Value = "Berufsschule Lenzbrug";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.28680419921875D), Telerik.Reporting.Drawing.Unit.Cm(0.20000000298023224D));
            this.pictureBox1.MimeType = "image/png";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2231945991516113D), Telerik.Reporting.Drawing.Unit.Cm(1.1999998092651367D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0170831680297852D), Telerik.Reporting.Drawing.Unit.Cm(0.88791656494140625D));
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(12.979374885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.txtTitle.Style.Font.Bold = true;
            this.txtTitle.Value = "Artikel";
            // 
            // txtYear
            // 
            this.txtYear.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.88791656494140625D));
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D));
            this.txtYear.Style.Font.Bold = true;
            this.txtYear.Value = "";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1058331727981567D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Format = "{0:d}";
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.682291030883789D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8277082443237305D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.2000001072883606D);
            this.reportHeader.Name = "reportHeader";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.5917309522628784D);
            this.reportFooter.Name = "reportFooter";
            this.reportFooter.Style.Visible = false;
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.94124984741210938D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox23,
            this.textBox21,
            this.textBox19,
            this.textBox12,
            this.valueDataTextBox,
            this.barcodeDataTextBox,
            this.nameDataTextBox,
            this.textBox6});
            this.detail.Name = "detail";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = true;
            this.textBox23.Format = "{0:N2}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.008125305175781D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox23.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox23.StyleName = "Data";
            this.textBox23.Value = "=Fields.CumulatedDepreciation";
            // 
            // textBox21
            // 
            this.textBox21.CanGrow = true;
            this.textBox21.Format = "{0:N2}";
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.70625114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox21.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox21.StyleName = "Data";
            this.textBox21.Value = "=Fields.AverageDepreciation";
            // 
            // textBox19
            // 
            this.textBox19.CanGrow = true;
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.711041450500488D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(60D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox19.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox19.StyleName = "Data";
            this.textBox19.Value = "=Fields.AcquisitionDate";
            // 
            // textBox12
            // 
            this.textBox12.CanGrow = true;
            this.textBox12.Format = "{0:N2}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(19.310001373291016D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox12.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox12.StyleName = "Data";
            this.textBox12.Value = "=Fields.DepreciationValue";
            // 
            // valueDataTextBox
            // 
            this.valueDataTextBox.CanGrow = true;
            this.valueDataTextBox.Format = "{0:N2}";
            this.valueDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.404375076293945D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.valueDataTextBox.Name = "valueDataTextBox";
            this.valueDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(83.14959716796875D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.valueDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.valueDataTextBox.StyleName = "Data";
            this.valueDataTextBox.Value = "=Fields.Value";
            // 
            // barcodeDataTextBox
            // 
            this.barcodeDataTextBox.CanGrow = true;
            this.barcodeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.9064583778381348D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.barcodeDataTextBox.Name = "barcodeDataTextBox";
            this.barcodeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(100.77951049804688D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.barcodeDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.barcodeDataTextBox.StyleName = "Data";
            this.barcodeDataTextBox.Value = "=Fields.Barcode";
            // 
            // nameDataTextBox
            // 
            this.nameDataTextBox.CanGrow = true;
            this.nameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.nameDataTextBox.Name = "nameDataTextBox";
            this.nameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(188.97637939453125D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.nameDataTextBox.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.nameDataTextBox.StyleName = "Data";
            this.nameDataTextBox.Value = "=Fields.Name";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(64.251983642578125D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "Anzahl";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Pixel(64.251983642578125D), Telerik.Reporting.Drawing.Unit.Pixel(30D));
            this.textBox6.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Cm(0.10000000149011612D);
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Fields.ArticleAmount";
            // 
            // KategorieGroupList
            // 
            group1.GroupFooter = this.roomFloorBuildingGroupFooterSection;
            group1.GroupHeader = this.roomFloorBuildingGroupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.DepreciationCategory"));
            group1.Name = "roomFloorBuildingGroup";
            group2.GroupFooter = this.labelsGroupFooterSection;
            group2.GroupHeader = this.labelsGroupHeaderSection;
            group2.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.roomFloorBuildingGroupHeaderSection,
            this.roomFloorBuildingGroupFooterSection,
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "RoomGroupingReport";
            this.PageSettings.Landscape = true;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule1.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule1.Style.Font.Name = "Calibri";
            styleRule1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule2.Style.Font.Name = "Calibri";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule3.Style.Font.Name = "Calibri";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule4.Style.Font.Name = "Calibri";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(21.600000381469727D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.GroupHeaderSection roomFloorBuildingGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.GroupFooterSection roomFloorBuildingGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox nameCaptionTextBox;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox valueCaptionTextBox;
        private Telerik.Reporting.TextBox barcodeCaptionTextBox;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox valueDataTextBox;
        private Telerik.Reporting.TextBox barcodeDataTextBox;
        private Telerik.Reporting.TextBox nameDataTextBox;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox txtRoomTotal;
        private Telerik.Reporting.TextBox txtTitle;
        private Telerik.Reporting.TextBox txtYear;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;

    }
}
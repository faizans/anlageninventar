using Client.Site.Controls.UserSearchControl;
using Client.SiteMaster;
using Client.Util;
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Client.Site.Administrator {
    public partial class ManageArticle : System.Web.UI.Page {

        #region Properties

        private Article article;

        #region FIELD SESSIONS
        private Boolean UseSessionFields {
            get {
                if (Session["UseSessionFields"] != null) {
                    return Boolean.Parse(Session["UseSessionFields"].ToString());
                }
                return false;
            }
            set {
                Session["UseSessionFields"] = value;
            }
        }
        private String Name {
            get {
                return Session["Name"].ToString();
            }
            set {
                Session["Name"] = value;
            }
        }
        private double Amount {
            get {
                if (Session["Amount"] != null) {
                    return double.Parse(Session["Amount"].ToString());
                }
                return 1;
            }
            set {
                Session["Amount"] = value;
            }
        }
        private String Barcode {
            get {
                return Session["Barcode"].ToString();
            }
            set {
                Session["Barcode"] = value;
            }
        }
        private String GroupName {
            get {
                return Session["GroupName"].ToString();
            }
            set {
                Session["GroupName"] = value;
            }
        }
        private String GroupBarCode {
            get {
                return Session["GroupBarCode"].ToString();
            }
            set {
                Session["GroupBarCode"] = value;
            }
        }
        private double Price {
            get {
                if (Session["Price"] != null) {
                    return double.Parse(Session["Price"].ToString());
                }
                return 0;
            }
            set {
                Session["Price"] = value;
            }
        }
        private DateTime AcquisitionDate {
            get {
                if (Session["AcquisitionDate"] != null) {
                    return (DateTime)Session["AcquisitionDate"];
                }
                return DateTime.Now;
            }
            set {
                Session["AcquisitionDate"] = value;
            }
        }
        private int BuildingId {
            get {
                if (Session["BuildingId"] != null) {
                    return int.Parse(Session["BuildingId"].ToString());
                }
                return -1;
            }
            set {
                Session["BuildingId"] = value;
            }
        }
        private int FloorId {
            get {
                if (Session["FloorId"] != null) {
                    return int.Parse(Session["FloorId"].ToString());
                }
                return -1;
            }
            set {
                Session["FloorId"] = value;
            }
        }
        private int RoomId {
            get {
                if (Session["RoomId"] != null) {
                    return int.Parse(Session["RoomId"].ToString());
                }
                return -1;
            }
            set {
                Session["RoomId"] = value;
            }
        }
        private int SupplierId {
            get {
                if (Session["SupplierId"] != null) {
                    return int.Parse(Session["SupplierId"].ToString());
                }
                return -1;
            }
            set {
                Session["SupplierId"] = value;
            }
        }
        private int SupplierBranchId {
            get {
                if (Session["SupplierBranchId"] != null) {
                    return int.Parse(Session["SupplierBranchId"].ToString());
                }
                return -1;
            }
            set {
                Session["SupplierBranchId"] = value;
            }
        }
        private int ArticleCategoryId {
            get {
                if (Session["ArticleCategoryId"] != null) {
                    return int.Parse(Session["ArticleCategoryId"].ToString());
                }
                return -1;
            }
            set {
                Session["ArticleCategoryId"] = value;
            }
        }
        private int InsuranceCategoryId {
            get {
                if (Session["InsuranceCategoryId"] != null) {
                    return int.Parse(Session["InsuranceCategoryId"].ToString());
                }
                return -1;
            }
            set {
                Session["InsuranceCategoryId"] = value;
            }
        }
        private int DepreciationCategoryId {
            get {
                if (Session["DepreciationCategoryId"] != null) {
                    return int.Parse(Session["DepreciationCategoryId"].ToString());
                }
                return -1;
            }
            set {
                Session["DepreciationCategoryId"] = value;
            }
        }
        private int DepreciationId {
            get {
                if (Session["DepreciationId"] != null) {
                    return int.Parse(Session["DepreciationId"].ToString());
                }
                return -1;
            }
            set {
                Session["DepreciationId"] = value;
            }
        }
        private String OldBarcode {
            get {
                return Session["OldBarcode"].ToString();
            }
            set {
                Session["OldBarcode"] = value;
            }
        }
        private String Comment {
            get {
                return Session["Comment"].ToString();
            }
            set {
                Session["Comment"] = value;
            }
        }
        private Boolean IsAvailable {
            get {
                return Boolean.Parse(Session["IsAvailable"].ToString());
            }
            set {
                Session["IsAvailable"] = value;
            }
        }
        #endregion

        #endregion

        protected void Page_Load(object sender, EventArgs e) {
            //Check if the set user is allowed to access
            if (this.SiteMaster.User == null || !this.SiteMaster.User.IsAdmin || !this.SiteMaster.User.IsActive) {
                Response.Redirect(Constants.AUTHORIZATION_WINDOWS_LOGIN);
            }

            loadPage();
        }

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        #region Initialisation

        private void loadPage() {
            getParameters();
            if (!IsPostBack) {
                bindDataSource();
                bindEntityData(this.article);
                if (UseSessionFields) {
                    this.updateFieldsWithSessions();
                    this.clearSessionFields();
                }
            }
        }

        private void getParameters() {
            if (Request.QueryString["ai"] != null && Request.QueryString["ai"] != "") {
                int articleId = int.Parse(Request.QueryString["ai"]);
                this.article = Article.GetById(articleId);
            }
        }

        #region Session handling

        /// <summary>
        /// Clear the session fields
        /// </summary>
        private void clearSessionFields() {
            this.UseSessionFields = false;
            this.Name = null;
            this.Amount = 1;
            this.Barcode = null;
            this.GroupName = null;
            this.GroupBarCode = null;
            this.Price = 0;
            this.AcquisitionDate = DateTime.Now;
            this.BuildingId = -1;
            this.FloorId = -1;
            this.RoomId = -1;
            this.SupplierId = -1;
            this.SupplierBranchId = -1;
            this.ArticleCategoryId = -1;
            this.InsuranceCategoryId = -1;
            this.DepreciationCategoryId = -1;
            this.DepreciationId = -1;
            this.OldBarcode = null;
            this.Comment = null;
            this.IsAvailable = true;
        }

        /// <summary>
        /// Set the session fields with the current values
        /// </summary>
        private void setSessionFields() {
            this.UseSessionFields = true;
            this.Name = this.rtbName.Text;
            this.Amount = this.rtbAmount.Value.HasValue ? this.rtbAmount.Value.Value : 1;
            this.Barcode = this.rtbBarcode.Text;
            this.Price = this.rtbPrice.Value.HasValue ? this.rtbPrice.Value.Value : 0;
            this.AcquisitionDate = this.rdpAcquisitionDate.SelectedDate.HasValue ? this.rdpAcquisitionDate.SelectedDate.Value : DateTime.Now;
            this.BuildingId = this.rcbBuilding.SelectedValue != null && this.rcbBuilding.SelectedValue  != ""? int.Parse(this.rcbBuilding.SelectedValue) : -1;
            this.FloorId = this.rcbFloor.SelectedValue != null && this.rcbFloor.SelectedValue != "" ? int.Parse(this.rcbFloor.SelectedValue) : -1;
            this.RoomId = this.rcbRoom.SelectedValue != null && this.rcbRoom.SelectedValue != "" ? int.Parse(this.rcbRoom.SelectedValue) : -1;
            this.SupplierId = this.rcbSupplier.SelectedValue != null && this.rcbSupplier.SelectedValue != "" && this.rcbSupplier.SelectedValue != "" ? int.Parse(this.rcbSupplier.SelectedValue) : -1;
            this.SupplierBranchId = this.rcbSupplierBranch.SelectedValue != null && this.rcbSupplierBranch.SelectedValue != "" ? int.Parse(this.rcbSupplierBranch.SelectedValue) : -1;
            this.ArticleCategoryId = this.rcbArticleCategory.SelectedValue != null && this.rcbArticleCategory.SelectedValue != "" ? int.Parse(this.rcbArticleCategory.SelectedValue) : -1;
            this.InsuranceCategoryId = this.rcbInsuranceCategory.SelectedValue != null && this.rcbInsuranceCategory.SelectedValue != "" ? int.Parse(this.rcbInsuranceCategory.SelectedValue) : -1;
            this.DepreciationCategoryId = this.rcbDepreciationCategory.SelectedValue != null && this.rcbDepreciationCategory.SelectedValue != "" ? int.Parse(this.rcbDepreciationCategory.SelectedValue) : -1;
            this.OldBarcode = this.rtbOldBarcode.Text;
            this.Comment = this.rtbComment.Text;
            this.IsAvailable = this.chbIsAvailable.Checked;
        }

        private void updateFieldsWithSessions() {
            this.rtbName.Text = this.Name;
            this.rtbAmount.Value = this.Amount;
            this.rtbBarcode.Text = this.Barcode;
            this.rtbPrice.Value = this.Price;
            this.rdpAcquisitionDate.SelectedDate = this.AcquisitionDate;
            this.rcbBuilding.SelectedValue = this.BuildingId.ToString();
            this.rcbFloor.SelectedValue = this.FloorId.ToString();
            this.rcbRoom.SelectedValue = this.RoomId.ToString();
            this.rcbSupplier.SelectedValue = this.SupplierId.ToString();
            this.rcbSupplierBranch.SelectedValue = this.SupplierBranchId.ToString();
            this.rcbArticleCategory.SelectedValue = this.ArticleCategoryId.ToString();
            this.rcbInsuranceCategory.SelectedValue = this.InsuranceCategoryId.ToString();
            this.rcbDepreciationCategory.SelectedValue = this.DepreciationCategoryId.ToString();
            this.rtbOldBarcode.Text = this.OldBarcode;
            this.rtbComment.Text = this.Comment;
            this.chbIsAvailable.Checked = this.IsAvailable;
        }

        #endregion

        private void bindDataSource() {
            //ArticleCategory
            IEnumerable<ArticleCategory> articleCategories = ArticleCategory.GetAll();
            this.rcbArticleCategory.DataSource = articleCategories != null ? articleCategories.ToList() : null;
            this.rcbArticleCategory.DataBind();
            if (articleCategories.Any() && articleCategories.Where(i => (i.IsDefault.HasValue && i.IsDefault.Value)).Any()) {
                rcbArticleCategory.Items.Where(i => i.Value ==
                    articleCategories.Where(a => (a.IsDefault.HasValue && a.IsDefault.Value)).ElementAt(0).ArticleCategoryId.ToString()).SingleOrDefault().Selected = true;
            }


            //InsuranceCategory
            IEnumerable<InsuranceCategory> insuranceCategories = InsuranceCategory.GetAll();
            this.rcbInsuranceCategory.DataSource = insuranceCategories != null ? insuranceCategories.ToList() : null;
            this.rcbInsuranceCategory.DataBind();
            if (insuranceCategories.Any() && insuranceCategories.Where(i => (i.IsDefault.HasValue && i.IsDefault.Value)).Any()) {
                RadComboBoxItem selectedItem = rcbInsuranceCategory.Items.Where(i => i.Value ==
                    insuranceCategories.Where(a => (a.IsDefault.HasValue && a.IsDefault.Value)).ElementAt(0).InsuranceCategoryId.ToString()).SingleOrDefault();
                selectedItem.Selected = true;
            }

            //DepreciationCategory
            IEnumerable<DepreciationCategory> depreciationCategories = DepreciationCategory.GetAll();
            this.rcbDepreciationCategory.DataSource = depreciationCategories != null ? depreciationCategories.ToList() : null;
            this.rcbDepreciationCategory.DataBind();
            if (depreciationCategories.Any() && depreciationCategories.Where(i => (i.IsDefault.HasValue && i.IsDefault.Value)).Any()) {
                rcbDepreciationCategory.Items.Where(i => i.Value ==
                    depreciationCategories.Where(a => (a.IsDefault.HasValue && a.IsDefault.Value)).ElementAt(0).DepreciationCategoryId.ToString()).SingleOrDefault().Selected = true;
            }

            //Supplier
            this.rcbSupplier.DataSource = Supplier.GetAll().ToList();
            this.rcbSupplier.DataBind();

            //Building 
            this.rcbBuilding.DataSource = Building.GetAll().ToList();
            this.rcbBuilding.DataBind();
        }

        private void bindEntityData(Article article) {
            if (article != null) {
                //TExtboxes
                this.rtbName.Text = article.Name;
                this.rtbBarcode.Text = article.Barcode;
                this.rtbOldBarcode.Text = article.OldBarcode;
                this.rtbAmount.Value = 1;
                this.rtbAmount.Enabled = false;
                this.rtbComment.Text = article.Comment;

                if (this.article.ArticleGroup != null) {

                    //Article Group
                    if (article.ArticleGroup != null && article.ArticleGroup.ArticleCategory != null) {
                        this.rcbArticleCategory.Items.Where(i => i.Text == article.ArticleGroup.ArticleCategory.Name).SingleOrDefault().Selected = true;
                    }
                }

                this.rtbPrice.Value = article.Value;
                this.rdpAcquisitionDate.SelectedDate = article.AcquisitionDate;

                //INsuranceCategory
                if (article.InsuranceCategory != null) {
                    this.rcbInsuranceCategory.Items.Where(i => i.Text == article.InsuranceCategory.Name).SingleOrDefault().Selected = true;
                }
                //DepreciationCategory and depreciation
                if (article.DepreciationCategory != null) {
                    this.rcbDepreciationCategory.Items.Where(i => i.Text == article.DepreciationCategory.Name).SingleOrDefault().Selected = true;
                    //bindDepreciationData(this.rcbDepreciationCategory.SelectedItem.Value);
                    //if (this.rcbDepreciationInterval.Items.Any()) {
                    //    this.rcbDepreciationInterval.Items.Where(i => i.Text == article.Depreciation.Name).SingleOrDefault().Selected = true;
                    //}
                }
                //Supplier and Supplierbranch
                if (article.SupplierBranch != null) {
                    this.rcbSupplier.Items.Where(i => i.Text == article.SupplierBranch.Supplier.Name).SingleOrDefault().Selected = true;
                    bindSupplierBranchData(this.rcbSupplier.SelectedItem.Value);
                    if (this.rcbSupplierBranch.Items.Any()) {
                        this.rcbSupplierBranch.Items.Where(i => i.Text == article.SupplierBranch.Name).SingleOrDefault().Selected = true;
                    }
                }
                //Building, Floor and Room
                if (article.Room != null) {
                    this.rcbBuilding.Items.Where(i => i.Text == article.Room.Floor.Building.Name).SingleOrDefault().Selected = true;
                    bindFloorData(this.rcbBuilding.SelectedItem.Value);
                    this.rcbFloor.Items.Where(i => i.Text == article.Room.Floor.Name).SingleOrDefault().Selected = true;
                    bindRoomData(this.rcbFloor.SelectedItem.Value);
                    this.rcbRoom.Items.Where(i => i.Text == article.Room.Name).SingleOrDefault().Selected = true;
                }

                //Checkboxes
                this.chbIsAvailable.Checked = article.IsAvailable.Value;

                //visibilities
                this.BarCodePanel.Visible = true;
                this.rtbPrice.ReadOnly = true;
            } else {
                this.IsAvailablePanel.Visible = false;

                if (UseSessionFields) {
                    //DepreciationCategory and depreciation
                    RadComboBoxItem selectedDepreciationItem = this.rcbDepreciationCategory.Items.Where(i => i.Value == this.DepreciationCategoryId.ToString()).SingleOrDefault();
                    if (selectedDepreciationItem != null) {
                        selectedDepreciationItem.Selected = true;
                    }

                    //Supplier and SupplierBranch
                    RadComboBoxItem selectedSupplierItem = this.rcbSupplier.Items.Where(i => i.Value == this.SupplierId.ToString()).SingleOrDefault();
                    if (selectedSupplierItem != null) {
                        selectedSupplierItem.Selected = true;

                        bindSupplierBranchData(this.rcbSupplier.SelectedItem.Value);
                        
                        if(this.rcbSupplierBranch.Items.Any()){
                            this.rcbSupplierBranch.Items.Where(i => i.Value == this.SupplierBranchId.ToString()).SingleOrDefault().Selected = true;
                        }
                    }

                    //Building, Floor and Room
                    RadComboBoxItem selectedBuildingItem = this.rcbBuilding.Items.Where(i => i.Value == this.BuildingId.ToString()).SingleOrDefault();
                    if (selectedBuildingItem != null) {
                        selectedBuildingItem.Selected = true;
                        bindFloorData(selectedBuildingItem.Value);
                    }
                    
                    RadComboBoxItem selectedFloorItem= this.rcbFloor.Items.Where(i => i.Value == this.FloorId.ToString()).SingleOrDefault();
                    if (selectedFloorItem != null) {
                        selectedFloorItem.Selected = true;
                        bindRoomData(selectedFloorItem.Value);
                    }

                    RadComboBoxItem selectedRoomItem= this.rcbRoom.Items.Where(i => i.Value == this.RoomId.ToString()).SingleOrDefault();
                    if (selectedRoomItem != null) {
                        selectedRoomItem.Selected = true;
                    }
                }
            }
        }

        private void save() {
            if (this.article == null) {
                //Do barcode preperations
                String groupBarCode = LatestBarCode.GenerateFullBarCode();
                LatestBarCode.Set(groupBarCode);

                String barCodeCounterPart = groupBarCode != "" && groupBarCode != null ? groupBarCode.Split('.')[groupBarCode.Split('.').Length - 1] : null;
                int barCode = barCodeCounterPart != "" && barCodeCounterPart != null ? int.Parse(barCodeCounterPart) : -1;
                int barCodeDigits = barCode > -1 ? barCode.ToString().Length : -1;

                ArticleGroup group = new ArticleGroup();
                //Set the group name
                if (this.rcbArticleCategory.SelectedValue != null && this.rcbArticleCategory.SelectedValue != "") {
                    group.ArticleCategoryId = int.Parse(this.rcbArticleCategory.SelectedValue);
                }
                group.Barcode = groupBarCode;
                group.RoomId = int.Parse(this.rcbRoom.SelectedItem.Value);

                //If amount is bigger than 1 create entites regarding to amount.
                for (int i = 1; i <= this.rtbAmount.Value; i++) {
                    this.article = new Article();
                    this.article.LastChangest = DateTime.Now;

                    setData(this.article, groupBarCode);

                    if (group != null) {
                        this.article.ArticleGroup = group;

                        barCode++;
                        //Handle Barcode
                        String newBarCodeEnd = barCodeCounterPart.Remove(barCodeDigits.ToString().Length - 1, barCode.ToString().Length) + barCode;

                        //Override articles barcode
                        this.article.Barcode = groupBarCode.Remove((groupBarCode.Length) - (barCodeCounterPart.Length), barCodeCounterPart.Length)
                            + newBarCodeEnd;
                    }
                    EntityFactory.Context.Articles.Add(this.article);
                }
            } else {
                setData(this.article, this.article.Barcode);
                //Set the group name
                if (this.rcbArticleCategory.SelectedValue != null && this.rcbArticleCategory.SelectedValue != "") {
                    article.ArticleGroup.ArticleCategoryId = int.Parse(this.rcbArticleCategory.SelectedValue);
                }
            }

            EntityFactory.Context.SaveChanges();
        }

        private void setData(Article article, String barCode) {
            this.article.Barcode = barCode;
            this.article.Name = this.rtbName.Text;
            this.article.AcquisitionDate = this.rdpAcquisitionDate.SelectedDate;
            this.article.Amount = 1;
            this.article.Value = this.rtbPrice.Value / this.rtbAmount.Value; //Price / Amount = price per unit
            this.article.Comment = this.rtbComment.Text;
            this.article.IsAvailable = this.chbIsAvailable.Checked;
            this.article.OldBarcode = this.rtbOldBarcode.Text;

            if (this.rcbArticleCategory.SelectedItem != null && this.article.ArticleGroup != null)
                this.article.ArticleGroup.ArticleCategoryId = int.Parse(this.rcbArticleCategory.SelectedItem.Value);

            if (this.rcbDepreciationCategory.SelectedItem != null)
                this.article.DepreciationCategoryId = int.Parse(this.rcbDepreciationCategory.SelectedItem.Value);

            if (this.rcbInsuranceCategory.SelectedItem != null)
                this.article.InsuranceCategoryId = int.Parse(this.rcbInsuranceCategory.SelectedItem.Value);

            if (this.rcbSupplierBranch.SelectedItem != null)
                this.article.SupplierBranchId = int.Parse(this.rcbSupplierBranch.SelectedItem.Value);

            if (this.rcbRoom.SelectedItem != null)
                this.article.RoomId = int.Parse(this.rcbRoom.SelectedItem.Value);
        }

        #endregion

        #region Events

        protected void rtbAmount_TextChanged(object sender, EventArgs e) {
            
        }

        protected void rcbDepreciationCategory_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {
            bindDepreciationData(e.Value);
        }

        private void bindDepreciationData(string id) {
            int depreciationCategoryId = int.Parse(id);
            DepreciationCategory dc = DepreciationCategory.GetById(depreciationCategoryId);
        }

        protected void rcbLieferant_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {
            bindSupplierBranchData(e.Value);
        }

        private void bindSupplierBranchData(string id) {
            int supplierBranchId = int.Parse(id);
            Supplier sb = Supplier.GetById(supplierBranchId);
            this.rcbSupplierBranch.DataSource = sb.SupplierBranches.OrderBy(s => s.Name).ToList();
            this.rcbSupplierBranch.DataBind();

            if (sb.SupplierBranches.Count == 1) {
                this.rcbSupplierBranch.Items.ElementAt(0).Selected = true;
            }
        }

        protected void rcbBuilding_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {
            bindFloorData(e.Value);
        }

        private void bindFloorData(string id) {
            int buildingId = int.Parse(id);
            Building building = Building.GetById(buildingId);
            this.rcbFloor.DataSource = building.Floors.ToList();
            this.rcbFloor.DataBind();
        }

        protected void rcbFloor_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {
            bindRoomData(e.Value);
        }

        private void bindRoomData(string id) {
            int floorId = int.Parse(id);
            Floor floor = Floor.GetById(floorId);
            this.rcbRoom.DataSource = floor.Rooms.ToList();
            this.rcbRoom.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("~/Site/Administrator/ArticleList.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            save();
            Response.Redirect("~/Site/Administrator/ArticleList.aspx");
        }

        protected void btnCreateSupplier_Click(object sender, EventArgs e) {
            setSessionFields();
            Response.Redirect("~/Site/Administrator/ManageSupplier.aspx?back=ManageArticle");
        }

        #endregion
    }
}
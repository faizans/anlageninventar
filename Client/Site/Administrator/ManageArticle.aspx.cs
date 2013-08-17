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

        private Article article;

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
            }
        }

        private void getParameters() {
            if (Request.QueryString["ai"] != null && Request.QueryString["ai"] != "") {
                int articleId = int.Parse(Request.QueryString["ai"]);
                this.article = Article.GetById(articleId);
            }
        }

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
                    this.rtbGroupBarcode.Text = article.ArticleGroup.Barcode;
                    this.rtbGroupName.Text = article.ArticleGroup.Name;
                }

                this.rtbPrice.Value = article.Value;
                this.rdpAcquisitionDate.SelectedDate = article.AcquisitionDate;

                //ArticleCategory
                if (article.ArticleCategory != null) {
                    this.rcbArticleCategory.Items.Where(i => i.Text == article.ArticleCategory.Name).SingleOrDefault().Selected = true;
                }
                //INsuranceCategory
                if (article.InsuranceCategory != null) {
                    this.rcbInsuranceCategory.Items.Where(i => i.Text == article.InsuranceCategory.Name).SingleOrDefault().Selected = true;
                }
                //DepreciationCategory and depreciation
                if (article.Depreciation != null) {
                    this.rcbDepreciationCategory.Items.Where(i => i.Text == article.Depreciation.DepreciationCategory.Name).SingleOrDefault().Selected = true;
                    bindDepreciationData(this.rcbDepreciationCategory.SelectedItem.Value);
                    if (this.rcbDepreciationInterval.Items.Any()) {
                        this.rcbDepreciationInterval.Items.Where(i => i.Text == article.Depreciation.Name).SingleOrDefault().Selected = true;
                    }
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
            } else {
                this.IsAvailablePanel.Visible = false;
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

                ArticleGroup group = null;
                //if amount is bigger than 1 -> Create article group
                if (rtbAmount.Value > 1) {
                    group = new ArticleGroup();
                    group.Name = this.rtbGroupName.Text;
                    group.Barcode = groupBarCode;
                    group.RoomId = int.Parse(this.rcbRoom.SelectedItem.Value);
                }

                //If amount is bigger than 1 create entites regarding to amount.
                for (int i = 1; i <= this.rtbAmount.Value; i++) {
                    this.article = new Article();

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
                setData(this.article,this.article.Barcode);
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

            if (this.rcbArticleCategory.SelectedItem != null)
                this.article.ArticleCategoryId = int.Parse(this.rcbArticleCategory.SelectedItem.Value);

            if (this.rcbDepreciationInterval.SelectedItem != null)
                this.article.DepreciationId = int.Parse(this.rcbDepreciationInterval.SelectedItem.Value);

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
            if (this.rtbAmount.Value > 1) {
                this.GroupPanel.Visible = true;
            } else {
                this.GroupPanel.Visible = false;
            }
        }

        protected void rcbDepreciationCategory_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {
            bindDepreciationData(e.Value);
        }

        private void bindDepreciationData(string id) {
            int depreciationCategoryId = int.Parse(id);
            DepreciationCategory dc = DepreciationCategory.GetById(depreciationCategoryId);
            this.rcbDepreciationInterval.DataSource = dc.Depreciations.ToList();
            this.rcbDepreciationInterval.DataBind();
        }

        protected void rcbLieferant_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {
            bindSupplierBranchData(e.Value);
        }

        private void bindSupplierBranchData(string id) {
            int supplierBranchId = int.Parse(id);
            Supplier sb = Supplier.GetById(supplierBranchId);
            this.rcbSupplierBranch.DataSource = sb.SupplierBranches.ToList();
            this.rcbSupplierBranch.DataBind();
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
            Response.Redirect("~/Site/Administrator/ManageSupplier.aspx?back=ManageArticle");
        }

        #endregion
    }
}
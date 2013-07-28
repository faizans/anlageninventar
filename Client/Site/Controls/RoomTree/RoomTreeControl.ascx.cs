﻿
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.Entity;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Data.Model.Diagram;
using Data.Model;
using System.Text;
using Client.SiteMaster;

namespace Client.Site.Controls.RoomTree {
    public partial class RoomTreeControl : System.Web.UI.UserControl {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                resetSession();
                initDataSource();
            }
        }

        #region Properties

        public CustomMaster SiteMaster {
            get {
                CustomMaster mm = (CustomMaster)Page.Master;
                return mm;
            }
        }

        /// <summary>
        /// Return the selected item from the session list, containing dataitem.
        /// Item is returned by value AND type of dataitem, because items of different types can have same id.
        /// </summary>
        public RoomTreeItem SelectedRoomTreeItem {
            get {
                if (this.SelectedItem != null) {
                    return this.RoomTreeItems.Where(i => i.Value == this.SelectedItem.Value
                        && (i.DataItem == null ||
                            (i.DataItem != null
                                && this.SelectedItem.Attributes["DataType"] == ObjectContext.GetObjectType(i.DataItem.GetType()).ToString())))
                                    .SingleOrDefault();
                }
                return null;
            }
        }

        public List<RoomTreeItem> CheckedItems {
            get {
                if (Session["CheckedItems"] == null) {
                    Session["CheckedItems"] = new List<RoomTreeItem>();
                }
                return Session["CheckedItems"] as List<RoomTreeItem>;
            }
            set {
                Session["CheckedItems"] = value;
            }
        }

        public List<RoomTreeItem> DataSource {
            set {
                this.RadTreeView1.DataSource = value;
                this.RadTreeView1.DataBind();
            }
        }

        public List<RoomTreeItem> RoomTreeItems {
            get {
                if (Session["RoomTreeItems"] == null) {
                    Session["RoomTreeItems"] = new List<RoomTreeItem>();
                }
                return Session["RoomTreeItems"] as List<RoomTreeItem>;
            }
            set {
                Session["RoomTreeItems"] = value;
            }
        }

        public RadTreeNode SelectedItem {
            get {
                if (Session["SelectedItem"] == null) {
                    return null;
                }
                return Session["SelectedItem"] as RadTreeNode;
            }
            set {
                Session["SelectedItem"] = value;
            }
        }

        #endregion

        private void resetSession() {
            this.CheckedItems = null;
            this.RoomTreeItems = null;
            this.SelectedItem = null;
        }

        /// <summary>
        /// Create Treestructured Datasource out of the entity objects
        /// </summary>
        private void initDataSource() {
            RoomTreeItem rootItem = new RoomTreeItem("Gebäude");
            //
            this.RoomTreeItems.Add(rootItem);

            //Buildings
            IEnumerable<Building> buildings = Building.GetAll();
            if (buildings.Any()) {
                int buildingId = 10000000;
                int floorId = 2000000;
                int roomId = 3000000;
                foreach (Building building in buildings) {
                    RoomTreeItem buildingItem = new RoomTreeItem(buildingId, -1, building.Name, building.BuildingId.ToString(), building);
                    //
                    this.RoomTreeItems.Add(buildingItem);

                    //Floors
                    IEnumerable<Floor> floors = building.Floors;
                    if (floors.Any()) {
                        foreach (Floor floor in floors) {
                            RoomTreeItem floorItem = new RoomTreeItem(floorId, buildingId, floor.Name, floor.FloorId.ToString(), floor);
                            //
                            this.RoomTreeItems.Add(floorItem);

                            //Rooms
                            IEnumerable<Room> rooms = floor.Rooms;
                            if (rooms.Any()) {
                                foreach (Room room in rooms) {
                                    RoomTreeItem roomItem = new RoomTreeItem(roomId, floorId, room.Name, room.RoomId.ToString(), room);
                                    //
                                    this.RoomTreeItems.Add(roomItem);
                                    roomId++;
                                }
                            }
                            floorId++;
                        }
                    }
                    buildingId++;
                }
            }
            this.DataSource = RoomTreeItems;
        }

        #region EditForm

        /// <summary>
        /// Change visibility of buttons regarding selected node
        /// </summary>
        private void toggleButtons() {

            if (this.SelectedRoomTreeItem != null || this.CheckedItems.Any()) {
                String dataItemType = this.SelectedRoomTreeItem.DataItem != null ?
                                            ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()).ToString() : null;
                if (this.SelectedRoomTreeItem.IsRoot) {
                    toggleButtons(true, false, false, false,true);
                } else if (dataItemType == typeof(Building).ToString()) {
                    toggleButtons(false, true, false, true,true);
                } else if (dataItemType == typeof(Floor).ToString()) {
                    toggleButtons(false, false, true, true,true);
                } else if (dataItemType == typeof(Room).ToString()) {
                    toggleButtons(false, false, false, true,true);
                }
            } else {
                toggleButtons(false, false, false, false,false);
            }
        }

        private void toggleButtons(bool enableBuilding, bool enableFloor, bool enableRoom, bool enableDelete, bool enableReport) {
            this.btnAddBuilding.Enabled = enableBuilding;
            this.btnAddFloor.Enabled = enableFloor;
            this.btnAddRoom.Enabled = enableRoom;
            this.btnDelete.Enabled = enableDelete;
            this.btnReport.Enabled = enableReport;
        }

        /// <summary>
        /// Update the form on the right side to edit node information
        /// </summary>
        private void updateEditForm() {
            if (this.SelectedItem != null && this.SelectedItem.Value != ""
                && this.SelectedRoomTreeItem != null && this.SelectedRoomTreeItem.DataItem != null) {

                if (this.SelectedRoomTreeItem.IsRoot) {
                    this.EditForm.Visible = false;
                } else {
                    this.EditForm.Visible = true;
                    this.txtNodeName.Text = this.RadTreeView1.SelectedNode.Text;

                    //Additional form for room
                    if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Room)) {
                        Room selectedRoom = this.SelectedRoomTreeItem.DataItem as Room;
                        this.ResponsibleAttribute.Visible = true;
                        this.UserSearchBox.Text = selectedRoom.ResponsiblePerson;
                    } else {
                        this.ResponsibleAttribute.Visible = false;
                    }
                }
            } else {
                this.EditForm.Visible = false;
                this.txtNodeName.Text = null;
            }
        }

        #endregion

        #region Events

        #region Tree

        protected void RadTreeView1_NodeClick(object sender, RadTreeNodeEventArgs e) {
            this.SelectedItem = e.Node;
            toggleButtons();
            e.Node.Expanded = true;
            this.updateEditForm();
        }

        #endregion

        #region EditForm

        protected void btnSave_Click(object sender, EventArgs e) {
            if (this.txtNodeName.Text.Any() && this.SelectedRoomTreeItem != null) {

                if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Building)) {
                    Building selectedBuilding = this.SelectedRoomTreeItem.DataItem as Building;
                    if (this.SelectedRoomTreeItem.Value.Contains("-")) {
                        EntityFactory.Context.Buildings.Add(selectedBuilding);
                    }
                    selectedBuilding.Name = this.txtNodeName.Text;

                } else if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Floor)) {
                    Floor selectedFloor = this.SelectedRoomTreeItem.DataItem as Floor;
                    if (this.SelectedRoomTreeItem.Value.Contains("-")) {
                        selectedFloor.Building = this.RoomTreeItems.Where(i => i.Value == this.SelectedRoomTreeItem.ParentNode.Value
                                                                            && ObjectContext.GetObjectType(i.DataItem.GetType()) == typeof(Building)).SingleOrDefault().DataItem as Building;
                        EntityFactory.Context.Floors.Add(selectedFloor);
                    }
                    selectedFloor.Name = this.txtNodeName.Text;
                } else if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Room)) {
                    Room selectedRoom = this.SelectedRoomTreeItem.DataItem as Room;
                    if (this.SelectedRoomTreeItem.Value.Contains("-")) {
                        selectedRoom.Floor = this.RoomTreeItems.Where(i => i.Value == this.SelectedRoomTreeItem.ParentNode.Value
                                                                            && ObjectContext.GetObjectType(i.DataItem.GetType()) == typeof(Floor)).SingleOrDefault().DataItem as Floor;
                        EntityFactory.Context.Rooms.Add(selectedRoom);
                    }
                    selectedRoom.Name = this.txtNodeName.Text;
                    selectedRoom.ResponsiblePerson = this.UserSearchBox.Text;
                }
                this.RadTreeView1.SelectedNode.Text = this.txtNodeName.Text;
                EntityFactory.Context.SaveChanges();
                this.SelectedItem = this.RadTreeView1.SelectedNode;
                toggleButtons();
            }
        }

        #endregion

        #region Buttons

        protected void btnDelete_Click(object sender, EventArgs e) {
            StringBuilder errorMessage = new StringBuilder();
            errorMessage.AppendLine("Einige Objekte könne nicht gelöschte werden:");
            Boolean showErrorMessage = false;

            if (this.CheckedItems != null) {
                foreach (RoomTreeItem roomTreeItem in this.CheckedItems) {

                    roomTreeItem.Selected = true;

                    if (ObjectContext.GetObjectType(roomTreeItem.DataItem.GetType()) == typeof(Building)) {
                        Building buildingToDelete = roomTreeItem.DataItem as Building;
                        if (!buildingToDelete.HasArticles()) {
                            buildingToDelete.Delete();
                            //Remove the item from radtree
                            this.RadTreeView1.GetAllNodes().Where(n => n.Value == roomTreeItem.Value
                                && ObjectContext.GetObjectType(n.DataItem.GetType()) == typeof(Building)).SingleOrDefault().Remove();
                            this.SelectedItem = null;
                        } else {
                            errorMessage.AppendLine(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Gebäude zugewiesen sind.", buildingToDelete.Name));
                            showErrorMessage = true;
                        }
                    } else if (ObjectContext.GetObjectType(roomTreeItem.DataItem.GetType()) == typeof(Floor)) {
                        Floor floorToDelete = roomTreeItem.DataItem as Floor;
                        if (!floorToDelete.HasArticles()) {
                            floorToDelete.Delete();
                            //Remove the item from radtree
                            this.RadTreeView1.GetAllNodes().Where(n => n.Value == roomTreeItem.Value
                                && ObjectContext.GetObjectType(n.DataItem.GetType()) == typeof(Floor)).SingleOrDefault().Remove();
                            this.SelectedItem = null;
                        } else {
                            errorMessage.AppendLine(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Stockwerk zugewiesen sind.", floorToDelete.Name));
                            showErrorMessage = true;
                        }
                    } else if (ObjectContext.GetObjectType(roomTreeItem.DataItem.GetType()) == typeof(Room)) {
                        Room roomToDelete = roomTreeItem.DataItem as Room;
                        if (!roomToDelete.HasArticles()) {
                            roomToDelete.Delete();
                            //Remove the item from radtree
                            this.RadTreeView1.GetAllNodes().Where(n => n.Value == roomTreeItem.Value
                                && n.Attributes["DataType"] == typeof(Room).ToString()).SingleOrDefault().Remove();
                            this.SelectedItem = null;
                        } else {
                            errorMessage.AppendLine(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Raum zugewiesen sind.", roomToDelete.Name));
                            showErrorMessage = true;
                        }
                    }
                }
                //Save all
                EntityFactory.Context.SaveChanges();

                //Reset the checked items
                this.CheckedItems = null;

                //update the edit forms
                updateEditForm();

                //Error MEssage
                if (showErrorMessage)
                    RadWindowManager1.RadAlert(errorMessage.ToString(), 300, 130, "Operation nicht möglich", "alertCallBackFn");
            }
        }

        protected void btnAddBuilding_Click(object sender, EventArgs e) {
            addNewNode(new Building());
        }

        protected void btnAddFloor_Click(object sender, EventArgs e) {
            addNewNode(new Floor());
        }

        protected void btnAddRoom_Click(object sender, EventArgs e) {
            addNewNode(new Room());
        }

        private void addNewNode(object dataItem) {
            RoomTreeItem newNode = new RoomTreeItem();
            newNode.DataItem = dataItem;
            newNode.Attributes["DataType"] = ObjectContext.GetObjectType(dataItem.GetType()).ToString();
            newNode.Value = Guid.NewGuid().ToString();
            newNode.Selected = true;

            this.SelectedItem = newNode;
            this.RadTreeView1.SelectedNode.Nodes.Add(newNode);
            this.RoomTreeItems.Add(newNode);

            this.updateEditForm();
            this.txtNodeName.Focus();
        }

        protected void btnReport_Click(object sender, EventArgs e) {
            //Reset the datasource first
            this.SiteMaster.ReportDataSource = null;

            //Go through all the checked items and add articles to report datasource
            if(this.CheckedItems.Any()){
                foreach (RoomTreeItem roomTreeItem in this.CheckedItems) {
                    if (ObjectContext.GetObjectType(roomTreeItem.DataItem.GetType()) == typeof(Building)) {
                        Building building = roomTreeItem.DataItem as Building;
                        this.SiteMaster.ReportDataSource.AddRange(building.Articles);
                    } else if (ObjectContext.GetObjectType(roomTreeItem.DataItem.GetType()) == typeof(Floor)) {
                        Floor floor = roomTreeItem.DataItem as Floor;
                        this.SiteMaster.ReportDataSource.AddRange(floor.Articles);
                    } else if (ObjectContext.GetObjectType(roomTreeItem.DataItem.GetType()) == typeof(Room)) {
                        Room room = roomTreeItem.DataItem as Room;
                        this.SiteMaster.ReportDataSource.AddRange(room.Articles);
                    }
                }
                this.CheckedItems = null;
                Response.Redirect("~/Site/Administrator/ReportView.aspx");
            }
            //Get selecteditem articles and add to report datasource
            else if(this.SelectedRoomTreeItem != null){
                if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Building)) {
                    Building building = this.SelectedRoomTreeItem.DataItem as Building;
                    this.SiteMaster.ReportDataSource.AddRange(building.Articles);
                } else if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Floor)) {
                    Floor floor = this.SelectedRoomTreeItem.DataItem as Floor;
                    this.SiteMaster.ReportDataSource.AddRange(floor.Articles);
                } else if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Room)) {
                    Room room = this.SelectedRoomTreeItem.DataItem as Room;
                    this.SiteMaster.ReportDataSource.AddRange(room.Articles);
                }
                this.CheckedItems = null;
                Response.Redirect("~/Site/Administrator/ReportView.aspx");
            }
        }

        #endregion

        protected void RadTreeView1_NodeDataBound(object sender, RadTreeNodeEventArgs e) {
            if (e.Node.DataItem != null && e.Node.DataItem.GetType() == typeof(RoomTreeItem)) {
                RoomTreeItem roomTreeNodeItem = e.Node.DataItem as RoomTreeItem;
                e.Node.Value = roomTreeNodeItem.Value;
                e.Node.Attributes["IsRoot"] = roomTreeNodeItem.IsRoot.ToString();
                if (roomTreeNodeItem.DataItem != null) {
                    e.Node.Attributes["DataType"] = ObjectContext.GetObjectType(roomTreeNodeItem.DataItem.GetType()).ToString();
                }
            }
        }

        protected void RadTreeView1_NodeCheck(object sender, RadTreeNodeEventArgs e) {
            if (e.Node.Value != null && e.Node.Value != "") {
                if (e.Node.Checked) {
                    e.Node.Selected = true;
                    this.SelectedItem = e.Node;
                    if (!this.CheckedItems.Contains(this.SelectedRoomTreeItem)) {
                        this.CheckedItems.Add(this.SelectedRoomTreeItem);
                    }
                } else {
                    this.CheckedItems.Remove(this.SelectedRoomTreeItem);
                }

                toggleButtons();
            }
        }

        #endregion

    }
}
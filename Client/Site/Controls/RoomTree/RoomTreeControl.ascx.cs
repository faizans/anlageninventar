
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

namespace Client.Site.Controls.RoomTree {
    public partial class RoomTreeControl : System.Web.UI.UserControl {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                initDataSource();
            }
        }

        #region Properties

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

            if (this.SelectedRoomTreeItem != null) {
                String dataItemType = this.SelectedRoomTreeItem.DataItem != null ?
                                            ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()).ToString() : null;
                if (this.SelectedRoomTreeItem.IsRoot) {
                    toggleButtons(true, false, false, false);
                } else if (dataItemType == typeof(Building).ToString()) {
                    toggleButtons(false, true, false, true);
                } else if (dataItemType == typeof(Floor).ToString()) {
                    toggleButtons(false, false, true, true);
                } else if (dataItemType == typeof(Room).ToString()) {
                    toggleButtons(false, false, false, true);
                }
            } else {
                toggleButtons(false, false, false, false);
            }
        }

        private void toggleButtons(bool enableBuilding, bool enableFloor, bool enableRoom, bool enableDelete) {
            this.btnAddBuilding.Enabled = enableBuilding;
            this.btnAddFloor.Enabled = enableFloor;
            this.btnAddRoom.Enabled = enableRoom;
            this.btnDelete.Enabled = enableDelete;
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
            if (this.SelectedRoomTreeItem != null) {
                if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Building)) {
                    Building buildingToDelete = this.SelectedRoomTreeItem.DataItem as Building;
                    if (!buildingToDelete.HasArticles()) {
                        buildingToDelete.Delete();
                        EntityFactory.Context.SaveChanges();
                        this.RadTreeView1.SelectedNode.Remove();
                        this.SelectedItem = null;
                    } else {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Gebäude zugewiesen sind.",buildingToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    }
                } else if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Floor)) {
                    Floor floorToDelete = this.SelectedRoomTreeItem.DataItem as Floor;
                    if (!floorToDelete.HasArticles()) {
                       floorToDelete.Delete();
                       EntityFactory.Context.SaveChanges();
                       this.RadTreeView1.SelectedNode.Remove();
                       this.SelectedItem = null;
                    } else {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Stockwerk zugewiesen sind.",floorToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    }
                } else if (ObjectContext.GetObjectType(this.SelectedRoomTreeItem.DataItem.GetType()) == typeof(Room)) {
                    Room roomToDelete = this.SelectedRoomTreeItem.DataItem as Room;
                    if (!roomToDelete.HasArticles()) {
                        roomToDelete.Delete();
                        EntityFactory.Context.SaveChanges();
                        this.RadTreeView1.SelectedNode.Remove();
                        this.SelectedItem = null;
                    } else {
                        RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Raum zugewiesen sind.", roomToDelete.Name), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                    }
                }
                updateEditForm();
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

        #endregion

    }
}
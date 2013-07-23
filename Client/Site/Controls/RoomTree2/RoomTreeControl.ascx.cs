
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

namespace Client.Site.Controls.RoomTree2
{
    public partial class RoomTreeControl : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initDataSource();
            }
        }

        #region Properties

        private RoomTreeItem selectedRoomTreeItem;
        public RoomTreeItem SelectedRoomTreeItem {
            get {
                if (this.RadTreeView1.SelectedNode != null) { 
                    return this.RoomTreeItems.Where(i=>i.Value == this.RadTreeView1.SelectedNode.Value).SingleOrDefault();
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

        #endregion

        private void initDataSource()
        {
            RoomTreeItem rootItem = new RoomTreeItem("Gebäude");
            //
            this.RoomTreeItems.Add(rootItem);

            //Buildings
            IEnumerable<Building> buildings = Building.GetAll();
            if (buildings.Any()) {
                int buildingId = 10000000;
                foreach (Building building in buildings) {
                    RoomTreeItem buildingItem = new RoomTreeItem(buildingId, -1, building.Name, building.BuildingId.ToString(), building);
                    //
                    this.RoomTreeItems.Add(buildingItem);

                    //Floors
                    IEnumerable<Floor> floors = building.Floors;
                    if (floors.Any()) {
                        int floorId = 2000000;
                        foreach (Floor floor in floors) {
                            RoomTreeItem floorItem = new RoomTreeItem(floorId, buildingId, floor.Name, floor.FloorId.ToString(), floor);
                            //
                            this.RoomTreeItems.Add(floorItem);

                            //Rooms
                            IEnumerable<Room> rooms = floor.Rooms;
                            if (rooms.Any()) {
                                int roomId = 3000000;
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
        private void toggleButtons()
        {

            String dataItemType = this.RadTreeView1.SelectedNode.Attributes["DataItemType"];
            bool isRoot = this.RadTreeView1.SelectedNode.Attributes["IsRoot"] != null ? Boolean.Parse(this.RadTreeView1.SelectedNode.Attributes["IsRoot"]) : false;

            if (this.RadTreeView1.SelectedNode != null)
            {
                if (isRoot)
                {
                    toggleButtons(true, false, false, false);
                }
                else if (dataItemType == typeof(Building).ToString())
                {
                    toggleButtons(false, true, false, true);
                }
                else if (dataItemType == typeof(Floor).ToString())
                {
                    toggleButtons(false, false, true, true);
                }
                else if (dataItemType == typeof(Room).ToString())
                {
                    toggleButtons(false, false,false, true);
                }
            }
            else
            {
                toggleButtons(false, false, false, false);
            }
        }

        private void toggleButtons(bool enableBuilding, bool enableFloor, bool enableRoom, bool enableDelete)
        {
            this.btnAddBuilding.Enabled = enableBuilding;
            this.btnAddFloor.Enabled = enableFloor;
            this.btnAddRoom.Enabled = enableRoom;
            this.btnDelete.Enabled = enableDelete;
        }

        /// <summary>
        /// Update the form on the right side to edit node information
        /// </summary>
        private void updateEditForm()
        {
            if (this.SelectedRoomTreeItem != null && this.SelectedRoomTreeItem.DataItem != null) {
                if (this.SelectedRoomTreeItem.DataItem.GetType() == typeof(Building)) {
                }
            }
        }

        #endregion

        #region Events

        #region Tree

        protected void RadTreeView1_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            toggleButtons();
            e.Node.Expanded = true;
            this.updateEditForm();
        }

        #endregion

        #region EditForm

        protected void btnSave_Click(object sender, EventArgs e)
        {

           
        }

        #endregion

        #region Buttons

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAddBuilding_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAddFloor_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

    }
}
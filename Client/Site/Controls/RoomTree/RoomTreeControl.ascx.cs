
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

namespace Client.Site.Controls.RoomTree
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

        private void initDataSource()
        {
            CustomTreeNodeItem rootNode = new CustomTreeNodeItem("Gebäude");
            this.DataSource.Add(rootNode);

            IEnumerable<Building> buildings = Building.GetAll();
            if (buildings.Any())
            {
                foreach (Building building in buildings)
                {
                    CustomTreeNodeItem buildingNode = new CustomTreeNodeItem(building.BuildingId, -1, building.Name, building);
                    this.dataSource.Add(buildingNode);

                    if (building.Floors.Any())
                    {
                        foreach (Floor floor in building.Floors)
                        {
                            CustomTreeNodeItem floorNode = new CustomTreeNodeItem(floor.FloorId, building.BuildingId, floor.Name, floor);
                            this.dataSource.Add(floorNode);

                            if (floor.Rooms.Any())
                            {
                                foreach (Room room in floor.Rooms)
                                {
                                    CustomTreeNodeItem roomNode = new CustomTreeNodeItem(room.RoomId, floor.FloorId, room.Name, room);
                                    this.dataSource.Add(roomNode);
                                }
                            }
                        }
                    }
                }
            }

            this.RadTreeView1.DataSource = this.dataSource;
            this.RadTreeView1.DataBind();
        }

        #region Properties

        private List<RadTreeNode> dataSource;
        public List<RadTreeNode> DataSource
        {
            get
            {
                if (dataSource == null)
                {
                    dataSource = new List<RadTreeNode>();
                }
                return this.dataSource;
            }
            set
            {
                this.dataSource = value;
            }
        }

        #endregion

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

        #region EditForm

        /// <summary>
        /// Update the form on the right side to edit node information
        /// </summary>
        private void updateEditForm()
        {

            bool isRoot = this.RadTreeView1.SelectedNode.Attributes["IsRoot"] != null ? Boolean.Parse(this.RadTreeView1.SelectedNode.Attributes["IsRoot"]) : false;
            String dataItemType = this.RadTreeView1.SelectedNode.Attributes["DataItemType"];

            if (isRoot)
            {
                this.EditForm.Visible = false;
            }
            else
            {
                this.EditForm.Visible = true;
                this.txtNodeName.Text = this.RadTreeView1.SelectedNode.Text;

                //Additional form for room
                if (dataItemType == typeof(Room).ToString())
                {
                    Room selectedRoom = Room.GetByName(this.txtNodeName.Text);
                    this.ResponsibleAttribute.Visible = true;
                    this.UserSearchBox.Text = selectedRoom != null ? selectedRoom.Name : "";
                }
                else
                {
                    this.ResponsibleAttribute.Visible = false;
                }
            }
        }

        #endregion

        #region Events

        protected void RadTreeView1_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            toggleButtons();
            e.Node.Expanded = true;
            this.updateEditForm();
        }

        protected void RadTreeView1_NodeDataBound(object sender, RadTreeNodeEventArgs e)
        {
            if (e.Node.DataItem != null && e.Node.DataItem.GetType() == typeof(CustomTreeNodeItem))
            {
                CustomTreeNodeItem nodeItem = e.Node.DataItem as CustomTreeNodeItem;
                nodeItem.AddAttributesTo(e.Node);
            }
        }

        private void addNewNode(Type type)
        {
            RadTreeNode newNode = new RadTreeNode("");
            newNode.Attributes["DataItemType"] = type.ToString();
            newNode.Selected = true;
            this.RadTreeView1.SelectedNode.Nodes.Add(newNode);
            this.updateEditForm();
            this.txtNodeName.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //Remove Node if selection changed after creating node without saving
            if (this.RadTreeView1.SelectedNode != null && this.RadTreeView1.SelectedNode.Text == null)
            {
                this.RadTreeView1.SelectedNode.Remove();
            }

            String dataItemType = this.RadTreeView1.SelectedNode.Attributes["DataItemType"];
            String id = this.RadTreeView1.SelectedNode.Attributes["Id"];

            if (dataItemType == typeof(Building).ToString())
            {

                Building buildingToSave = null;
                if (id != null)
                {
                    buildingToSave = Building.GetById(int.Parse(id));
                }
                if (buildingToSave == null)
                {
                    buildingToSave = new Building();
                    buildingToSave.BuildingId = int.Parse("1" + Building.GetAll().Count());
                    EntityFactory.Context.Buildings.Add(buildingToSave);
                }
                buildingToSave.Name = this.txtNodeName.Text;

                EntityFactory.Context.SaveChanges();

                this.RadTreeView1.SelectedNode.Text = this.txtNodeName.Text;
                this.RadTreeView1.SelectedNode.Attributes["Id"] = buildingToSave.BuildingId.ToString();

            }
            else if (dataItemType == typeof(Floor).ToString())
            {

                Floor floorToSave = null;
                if (id != null)
                {
                    floorToSave = Floor.GetById(int.Parse(id));
                }
                if (floorToSave == null)
                {
                    floorToSave = new Floor();
                    floorToSave.FloorId = int.Parse("2" + Floor.GetAll().Count());

                    if (this.RadTreeView1.SelectedNode.ParentNode != null)
                    {
                        floorToSave.BuildingId = int.Parse(this.RadTreeView1.SelectedNode.ParentNode.Attributes["Id"]);
                    }

                    EntityFactory.Context.Floors.Add(floorToSave);

                    this.RadTreeView1.SelectedNode.Text = this.txtNodeName.Text;
                    this.RadTreeView1.SelectedNode.Attributes["Id"] = floorToSave.FloorId.ToString();
                }

                floorToSave.Name = this.txtNodeName.Text;

                EntityFactory.Context.SaveChanges();
            }
            else if (dataItemType == typeof(Room).ToString())
            {

                Room roomToSave = null;
                if (id != null)
                {
                    roomToSave = Room.GetById(int.Parse(id));
                }
                if (roomToSave == null)
                {
                    roomToSave = new Room();
                    roomToSave.RoomId = int.Parse("3" + Room.GetAll().Count());

                    if (this.RadTreeView1.SelectedNode.ParentNode != null)
                    {
                        roomToSave.FloorId = int.Parse(this.RadTreeView1.SelectedNode.ParentNode.Attributes["Id"]);
                    }

                    EntityFactory.Context.Rooms.Add(roomToSave);

                    this.RadTreeView1.SelectedNode.Text = this.txtNodeName.Text;
                    this.RadTreeView1.SelectedNode.Attributes["Id"] = roomToSave.FloorId.ToString();
                }

                roomToSave.Name = this.txtNodeName.Text;
                AppUser responsibleUser = AppUser.GetByEmail(this.UserSearchBox.Text);
                if (responsibleUser != null)
                {
                    if (roomToSave.AppUsers.Where(a => a.AppUserId == responsibleUser.AppUserId).Any())
                    {
                        roomToSave.AppUsers.Remove(roomToSave.AppUsers.Where(a => a.AppUserId == responsibleUser.AppUserId).SingleOrDefault());
                    }
                    roomToSave.AppUsers.Add(responsibleUser);
                }
                EntityFactory.Context.SaveChanges();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            String dataItemType = this.RadTreeView1.SelectedNode.Attributes["DataItemType"];
            String idString = this.RadTreeView1.SelectedNode.Attributes["Id"];

            if (idString != null)
            {
                int id = int.Parse(idString);
                if (dataItemType == typeof(Building).ToString())
                {
                    //Delete object from context
                    Building buildingToDelete = EntityFactory.Context.Buildings.Where(p => p.BuildingId == id).SingleOrDefault();
                    if (buildingToDelete != null)
                        if (!buildingToDelete.HasArticles())
                        {
                            buildingToDelete.Delete();
                        }
                        else
                        {
                            RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Raum zugewiesen sind."), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                        }
                }
                else if (dataItemType == typeof(Floor).ToString())
                {
                    //Delete object from context
                    Floor floorToDelete = EntityFactory.Context.Floors.Where(p => p.FloorId == id).SingleOrDefault();
                    if (floorToDelete != null)
                        if (!floorToDelete.HasArticles())
                        {
                            floorToDelete.Delete();
                        }
                        else
                        {
                            RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Raum zugewiesen sind."), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                        }
                }
                else if (dataItemType == typeof(Room).ToString())
                {
                    //Delete object from context
                    Room roomToDelete = EntityFactory.Context.Rooms.Where(p => p.RoomId == id).SingleOrDefault();
                    if (roomToDelete != null)
                        if (!roomToDelete.HasArticles())
                        {
                            roomToDelete.Delete();
                        }
                        else
                        {
                            RadWindowManager1.RadAlert(String.Format("{0} kann nicht gelöscht werden, da Artikel dem Raum zugewiesen sind."), 300, 130, "Operation nicht möglich", "alertCallBackFn");
                        }
                }
                EntityFactory.Context.SaveChanges();
                this.RadTreeView1.SelectedNode.Remove();
                this.EditForm.Visible = false;
            }
        }

        protected void btnAddBuilding_Click(object sender, EventArgs e)
        {
            addNewNode(ObjectContext.GetObjectType(typeof(Building)));
        }

        protected void btnAddFloor_Click(object sender, EventArgs e)
        {
            addNewNode(ObjectContext.GetObjectType(typeof(Floor)));
        }

        protected void btnAddRoom_Click(object sender, EventArgs e)
        {
            addNewNode(ObjectContext.GetObjectType(typeof(Room)));
        }

        #endregion

    }
}
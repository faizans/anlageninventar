
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram {
    public partial class Room {
        public String RoomPath {
            get {
                Floor parent = Floor.GetById(this.FloorId);
                return parent.Building.Name + "/" + parent.Name + "/" + this.Name;
            }
        }

        public static Room GetById(int id) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Rooms.Where(c => c.RoomId == id).SingleOrDefault();
        }

        public static Room GetByName(string name) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Rooms.Where(c => c.Name == name).SingleOrDefault();
        }

        public static Room GetByNameAndParent(string name, int floorId) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Rooms.Where(c => c.Name == name && c.FloorId == floorId).SingleOrDefault();
        }

        public static IEnumerable<Room> GetByNameAndFloorAndBuilding(String name, String floorName, String buildingName) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Rooms.Where(c => c.Name == name && c.Floor.Name == floorName && c.Floor.Building.Name == buildingName);
        }

        public static IEnumerable<Room> GetAll() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Rooms;
        }

        public Boolean HasArticles() {
            return this.Articles.Any();
        }

        public IEnumerable<Article> AvailableArticles {
            get {
                return this.Articles.Where(a => !a.IsDeleted);
            }
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            ctx.Rooms.Remove(ctx.Rooms.Where(p => p.RoomId == this.RoomId).SingleOrDefault());
        }

    }
}

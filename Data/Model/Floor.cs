
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class Floor
    {
        public static Floor GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Floors.Where(c => c.FloorId == id).SingleOrDefault();
        }

        public static IEnumerable<Floor> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Floors;
        }

        public static IEnumerable<Floor> GetByNameAndBuilding(String name, String buildingName) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Floors.Where(c => c.Name == name && c.Building.Name == buildingName);
        }

        public Boolean HasArticles()
        {
            if (this.Rooms.Any())
            {
                foreach (Room room in this.Rooms)
                {
                    if (room.HasArticles())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<Article> Articles {
            get {
                List<Article> articles = new List<Article>();
                if (this.Rooms.Any()) {
                    foreach (Room room in this.Rooms) {
                        if (room.AvailableArticles.Any()) {
                            articles.AddRange(room.AvailableArticles);
                        }
                    }
                }
                return articles;
            }
        }

        public void Delete() {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            if (this.Rooms.Any()) {
                List<Room> rooms = new List<Room>(this.Rooms);
                foreach (Room room in rooms) {
                    room.Delete();
                }
            }
            ctx.Floors.Remove(ctx.Floors.Where(p => p.FloorId == this.FloorId).SingleOrDefault());
        }
    }
}

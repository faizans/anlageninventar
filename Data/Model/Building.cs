
using Data.Model;
using Data.Model.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model.Diagram
{
    public partial class Building
    {
        public static Building GetById(int id)
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Buildings.Where(c => c.BuildingId == id).SingleOrDefault();
        }

        public static IEnumerable<Building> GetAll()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Buildings;
        }

        public static Building GetByName(string name) {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            return ctx.Buildings.Where(n=>n.Name == name).SingleOrDefault();
        }

        public Boolean HasArticles()
        {
            if (this.Floors.Any())
            {
                
                foreach (Floor floor in this.Floors)
                {
                    if (floor.HasArticles())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Delete()
        {
            IP3AnlagenInventarEntities ctx = EntityFactory.Context;
            if (this.Floors.Any())
            {
                List<Floor> floors = new List<Floor>(this.Floors);
                foreach (Floor floor in floors)
                {
                    floor.Delete();
                }
            }
            ctx.Buildings.Remove(ctx.Buildings.Where(p => p.BuildingId == this.BuildingId).SingleOrDefault());
        }

        public List<Article> Articles {
            get {
                List<Article> articles = new List<Article>();
                if (this.Floors.Any()) {
                    foreach (Floor floor in this.Floors) {
                        if (floor.Articles.Any()) {
                            articles.AddRange(floor.Articles);
                        }
                    }
                }
                return articles;
            }
        }
    }
}

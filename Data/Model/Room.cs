//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Room
    {
        public Room()
        {
            this.Articles = new HashSet<Article>();
            this.ArticleGroups = new HashSet<ArticleGroup>();
            this.AppUsers = new HashSet<AppUser>();
        }
    
        public int RoomId { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public string Code { get; set; }
        public string Floor { get; set; }
    
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ArticleGroup> ArticleGroups { get; set; }
        public virtual Building Building { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.Model.Diagram
{
    using System;
    using System.Collections.Generic;
    
    public partial class ArticleGroup
    {
        public ArticleGroup()
        {
            this.Articles = new HashSet<Article>();
        }
    
        public int ArticleGroupId { get; set; }
        public string Name { get; set; }
        public int RoomId { get; set; }
        public string Barcode { get; set; }
    
        public virtual ICollection<Article> Articles { get; set; }
        public virtual Room Room { get; set; }
    }
}

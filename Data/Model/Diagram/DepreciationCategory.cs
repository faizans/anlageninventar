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
    
    public partial class DepreciationCategory
    {
        public DepreciationCategory()
        {
            this.Depreciations = new HashSet<Depreciation>();
        }
    
        public int DepreciationCategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<double> DepreciationSpan { get; set; }
    
        public virtual ICollection<Depreciation> Depreciations { get; set; }
    }
}

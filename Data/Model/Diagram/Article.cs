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
    
    public partial class Article
    {
        public Article()
        {
            this.Depreciations = new HashSet<Depreciation>();
        }
    
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Barcode { get; set; }
        public string OldBarcode { get; set; }
        public Nullable<int> ArticleGroupId { get; set; }
        public Nullable<int> SupplierBranchId { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<System.DateTime> AcquisitionDate { get; set; }
        public Nullable<int> DepreciationId { get; set; }
        public Nullable<int> ArticleCategoryId { get; set; }
        public Nullable<int> InsuranceCategoryId { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> IsAvailable { get; set; }
    
        public virtual ArticleCategory ArticleCategory { get; set; }
        public virtual ArticleGroup ArticleGroup { get; set; }
        public virtual Depreciation Depreciation { get; set; }
        public virtual InsuranceCategory InsuranceCategory { get; set; }
        public virtual Room Room { get; set; }
        public virtual SupplierBranch SupplierBranch { get; set; }
        public virtual ICollection<Depreciation> Depreciations { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IP3AnlagenInventarEntities : DbContext
    {
        public IP3AnlagenInventarEntities()
            : base("name=IP3AnlagenInventarEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleGroup> ArticleGroups { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Depreciation> Depreciations { get; set; }
        public DbSet<DepreciationCategory> DepreciationCategories { get; set; }
        public DbSet<FilterField> FilterFields { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<InsuranceCategory> InsuranceCategories { get; set; }
        public DbSet<LatestBarCode> LatestBarCodes { get; set; }
        public DbSet<PredefinedFilter> PredefinedFilters { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierBranch> SupplierBranches { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}

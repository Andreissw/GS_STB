﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GS_STB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FASEntities1 : DbContext
    {
        public FASEntities1()
            : base("name=FASEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FAS_App_ListForPC> FAS_App_ListForPC { get; set; }
        public virtual DbSet<FAS_Applications> FAS_Applications { get; set; }
        public virtual DbSet<FAS_Breaks> FAS_Breaks { get; set; }
        public virtual DbSet<FAS_CERT> FAS_CERT { get; set; }
        public virtual DbSet<FAS_Disassembly> FAS_Disassembly { get; set; }
        public virtual DbSet<FAS_ErrorCode> FAS_ErrorCode { get; set; }
        public virtual DbSet<FAS_ErrorCodeGroup> FAS_ErrorCodeGroup { get; set; }
        public virtual DbSet<FAS_GS_LOTs> FAS_GS_LOTs { get; set; }
        public virtual DbSet<FAS_HDCP> FAS_HDCP { get; set; }
        public virtual DbSet<FAS_LabelScenario> FAS_LabelScenario { get; set; }
        public virtual DbSet<FAS_Lines> FAS_Lines { get; set; }
        public virtual DbSet<FAS_LineType> FAS_LineType { get; set; }
        public virtual DbSet<FAS_Liter> FAS_Liter { get; set; }
        public virtual DbSet<FAS_Model_Type> FAS_Model_Type { get; set; }
        public virtual DbSet<FAS_Models> FAS_Models { get; set; }
        public virtual DbSet<FAS_OperationLog> FAS_OperationLog { get; set; }
        public virtual DbSet<FAS_PackingCounter> FAS_PackingCounter { get; set; }
        public virtual DbSet<FAS_PackingGS> FAS_PackingGS { get; set; }
        public virtual DbSet<FAS_SerialNumbers> FAS_SerialNumbers { get; set; }
        public virtual DbSet<FAS_ShiftPlan> FAS_ShiftPlan { get; set; }
        public virtual DbSet<FAS_ShiftsCounter> FAS_ShiftsCounter { get; set; }
        public virtual DbSet<FAS_ShiftsDescription> FAS_ShiftsDescription { get; set; }
        public virtual DbSet<FAS_Start> FAS_Start { get; set; }
        public virtual DbSet<FAS_Stations> FAS_Stations { get; set; }
        public virtual DbSet<FAS_TempSerialNumbers1> FAS_TempSerialNumbers1 { get; set; }
        public virtual DbSet<FAS_Upload> FAS_Upload { get; set; }
        public virtual DbSet<FAS_UserGroup> FAS_UserGroup { get; set; }
        public virtual DbSet<FAS_Users> FAS_Users { get; set; }
        public virtual DbSet<FAS_WeightStation> FAS_WeightStation { get; set; }
        public virtual DbSet<FAS_WorkingScenario> FAS_WorkingScenario { get; set; }
    }
}
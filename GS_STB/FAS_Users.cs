//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class FAS_Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FAS_Users()
        {
            this.FAS_GS_LOTs = new HashSet<FAS_GS_LOTs>();
            this.FAS_GS_LOTs1 = new HashSet<FAS_GS_LOTs>();
            this.FAS_PackingGS = new HashSet<FAS_PackingGS>();
            this.FAS_Start = new HashSet<FAS_Start>();
            this.FAS_Upload = new HashSet<FAS_Upload>();
        }
    
        public short UserID { get; set; }
        public string RFID { get; set; }
        public string UserName { get; set; }
        public bool IsActiv { get; set; }
        public byte UsersGroupID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAS_GS_LOTs> FAS_GS_LOTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAS_GS_LOTs> FAS_GS_LOTs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAS_PackingGS> FAS_PackingGS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAS_Start> FAS_Start { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAS_Upload> FAS_Upload { get; set; }
        public virtual FAS_UserGroup FAS_UserGroup { get; set; }
    }
}

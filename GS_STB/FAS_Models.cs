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
    
    public partial class FAS_Models
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FAS_Models()
        {
            this.FAS_GS_LOTs = new HashSet<FAS_GS_LOTs>();
        }
    
        public short ModelID { get; set; }
        public string ModelName { get; set; }
        public byte ModelTypeID { get; set; }
        public string DelaySetting { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAS_GS_LOTs> FAS_GS_LOTs { get; set; }
        public virtual FAS_Model_Type FAS_Model_Type { get; set; }
    }
}

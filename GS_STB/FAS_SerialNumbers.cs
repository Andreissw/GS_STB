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
    
    public partial class FAS_SerialNumbers
    {
        public int SerialNumber { get; set; }
        public Nullable<short> LOTID { get; set; }
        public Nullable<bool> IsUsed { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsUploaded { get; set; }
        public Nullable<bool> IsWeighted { get; set; }
        public Nullable<bool> IsPacked { get; set; }
        public Nullable<bool> InRepair { get; set; }
        public Nullable<short> PrintStationID { get; set; }
        public Nullable<int> FixedID { get; set; }
    
        public virtual FAS_CERT FAS_CERT { get; set; }
        public virtual FAS_HDCP FAS_HDCP { get; set; }
        public virtual FAS_PackingGS FAS_PackingGS { get; set; }
        public virtual FAS_Start FAS_Start { get; set; }
        public virtual FAS_Upload FAS_Upload { get; set; }
        public virtual FAS_WeightStation FAS_WeightStation { get; set; }
    }
}

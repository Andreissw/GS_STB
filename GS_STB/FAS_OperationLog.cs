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
    
    public partial class FAS_OperationLog
    {
        public int ID { get; set; }
        public int PCBID { get; set; }
        public byte ProductionAreaID { get; set; }
        public short StationID { get; set; }
        public short ApplicationID { get; set; }
        public System.DateTime StateCodeDate { get; set; }
        public short StateCodeByID { get; set; }
        public Nullable<int> SerialNumber { get; set; }
        public Nullable<bool> Reprint { get; set; }
        public Nullable<bool> ReUpload { get; set; }
        public Nullable<System.DateTime> OldLabelDate { get; set; }
        public Nullable<long> SmartCardId { get; set; }
    }
}

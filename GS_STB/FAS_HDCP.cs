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
    
    public partial class FAS_HDCP
    {
        public int SerialNumber { get; set; }
        public string HDCPName { get; set; }
        public byte[] HDCPContent { get; set; }
    
        public virtual FAS_SerialNumbers FAS_SerialNumbers { get; set; }
    }
}

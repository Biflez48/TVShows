//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TVshows.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Favorites
    {
        public int idFav { get; set; }
        public int idUs { get; set; }
        public int idSh { get; set; }
        public System.DateTime dtNotifyRefresh { get; set; }
    
        public virtual Shows Shows { get; set; }
        public virtual Users Users { get; set; }
    }
}

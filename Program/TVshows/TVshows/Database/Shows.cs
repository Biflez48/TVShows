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
    
    public partial class Shows
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shows()
        {
            this.Favorites = new HashSet<Favorites>();
            this.Television = new HashSet<Television>();
        }
    
        public int idSh { get; set; }
        public string NameSh { get; set; }
        public System.TimeSpan tDurationSh { get; set; }
        public string DescrioptionSh { get; set; }
        public Nullable<int> idCat { get; set; }
    
        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorites> Favorites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Television> Television { get; set; }
    }
}

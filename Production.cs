//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RickVowens
{
    using System;
    using System.Collections.Generic;
    
    public partial class Production
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Production()
        {
            this.ProductionContain = new HashSet<ProductionContain>();
        }
    
        public int IDDepartment { get; set; }
        public System.DateTime DateProduction { get; set; }
        public int CountOfWorkers { get; set; }
        public int IDProduction { get; set; }
        public string SendStatus { get; set; }
    
        public virtual Departaments Departaments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionContain> ProductionContain { get; set; }
    }
}

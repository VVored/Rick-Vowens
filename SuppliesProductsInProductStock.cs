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
    
    public partial class SuppliesProductsInProductStock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SuppliesProductsInProductStock()
        {
            this.SuppliesProductsInProductStockContains = new HashSet<SuppliesProductsInProductStockContains>();
        }
    
        public int IDSupply { get; set; }
        public System.DateTime Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuppliesProductsInProductStockContains> SuppliesProductsInProductStockContains { get; set; }
    }
}
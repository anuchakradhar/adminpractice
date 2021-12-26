//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace index.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.SubItems = new HashSet<SubItem>();
        }
    
        public long item_id { get; set; }
        public string item_name { get; set; }
        public Nullable<int> display_sequence_number { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<bool> is_deleted { get; set; }
        public Nullable<System.DateTime> created_date { get; set; }
        public string image_name { get; set; }
        public string image_path { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubItem> SubItems { get; set; }
    }
}

namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LokalMobilni")]
    public partial class LokalMobilni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LokalMobilni()
        {
            MobilniTelefon = new HashSet<MobilniTelefon>();
        }

        public int LokalMobilniId { get; set; }

        [Required]
        [StringLength(10)]
        public string LokalMob { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MobilniTelefon> MobilniTelefon { get; set; }
    }
}

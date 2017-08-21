namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipMobilni")]
    public partial class TipMobilni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipMobilni()
        {
            MobilniTelefon = new HashSet<MobilniTelefon>();
        }

        public int TipMobilniId { get; set; }

        [Required]
        [StringLength(10)]
        public string VrstaMobilni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MobilniTelefon> MobilniTelefon { get; set; }
    }
}

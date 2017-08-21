namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipFiskni")]
    public partial class TipFiskni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipFiskni()
        {
            FiksniTelefon = new HashSet<FiksniTelefon>();
        }

        [Key]
        public int TipFiksniId { get; set; }

        [Required]
        [StringLength(10)]
        public string VrstaFiksni { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FiksniTelefon> FiksniTelefon { get; set; }
    }
}

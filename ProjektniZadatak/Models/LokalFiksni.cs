namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LokalFiksni")]
    public partial class LokalFiksni
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LokalFiksni()
        {
            FiksniTelefon = new HashSet<FiksniTelefon>();
        }

        public int LokalFiksniId { get; set; }

        [Required]
        [StringLength(10)]
        public string LokalFik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FiksniTelefon> FiksniTelefon { get; set; }
    }
}

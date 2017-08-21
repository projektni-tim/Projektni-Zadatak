namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TipEmailAdrese
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipEmailAdrese()
        {
            EmailAdresa = new HashSet<EmailAdresa>();
        }

        public int TipEmailAdreseId { get; set; }

        [Required]
        [StringLength(10)]
        public string VrstaEmailAdrese { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailAdresa> EmailAdresa { get; set; }
    }
}

namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Opstina")]
    public partial class Opstina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opstina()
        {
            Osoba = new HashSet<Osoba>();
        }

        public int OpstinaId { get; set; }

       
        public string NazivOpstine { get; set; }

        public int GradId { get; set; }

        public virtual Grad Grad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Osoba> Osoba { get; set; }
    }
}

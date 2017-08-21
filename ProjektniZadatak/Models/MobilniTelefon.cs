namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobilniTelefon")]
    public partial class MobilniTelefon
    {
        public int MobilniTelefonId { get; set; }

        public int? LokalMobilniId { get; set; }

        public int? TipMobilniId { get; set; }

        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [RegularExpression("^[0-9]{6,7}$", ErrorMessage = "Broj nije ispravno unet")]
        public string BrojMobilnog { get; set; }

        public int OsobaId { get; set; }

        public virtual LokalMobilni LokalMobilni { get; set; }

        public virtual Osoba Osoba { get; set; }

        public virtual TipMobilni TipMobilni { get; set; }
    }
}

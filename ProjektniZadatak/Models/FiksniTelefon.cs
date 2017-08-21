namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FiksniTelefon")]
    public partial class FiksniTelefon
    {
        public int FiksniTelefonId { get; set; }

        public int? LokalFiksniId { get; set; }

        public int? TipFiksniId { get; set; }

        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [RegularExpression("^[0-9]{6,7}$", ErrorMessage = "Broj nije ispravno unet")]
        public string BrojFiksnog { get; set; }

        public int OsobaId { get; set; }

        public virtual LokalFiksni LokalFiksni { get; set; }

        public virtual Osoba Osoba { get; set; }

        public virtual TipFiskni TipFiskni { get; set; }
    }
}

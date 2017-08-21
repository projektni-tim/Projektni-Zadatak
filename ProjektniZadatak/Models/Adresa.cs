namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adresa")]
    public partial class Adresa
    {
        public int AdresaId { get; set; }

        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [MinLength(3, ErrorMessage = "Minimum 3 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
        [RegularExpression("^[0-9a-zA-ZšđčćžŠĐČĆŽ ]+$", ErrorMessage = "Nije ispravno uneta adresa")]
        public string NazivAdrese { get; set; }

        public int OsobaId { get; set; }

        public int TipAdreseId { get; set; }

        public int GradId { get; set; }

        public virtual Grad Grad { get; set; }

        public virtual Osoba Osoba { get; set; }

        public virtual TipAdrese TipAdrese { get; set; }
    }
}

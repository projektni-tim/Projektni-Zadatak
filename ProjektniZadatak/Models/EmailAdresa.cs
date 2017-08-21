namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailAdresa")]
    public partial class EmailAdresa
    {

        public int EmailAdresaId { get; set; }

        [Required(ErrorMessage = "Polje mora biti popunjeno")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email nije ispravno unet")]
        public string NazivEmailAdrese { get; set; }

        public int OsobaId { get; set; }

        public int TipEmailAdreseId { get; set; }

        public virtual Osoba Osoba { get; set; }

        public virtual TipEmailAdrese TipEmailAdrese { get; set; }
    }
}

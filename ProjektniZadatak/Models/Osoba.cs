namespace ProjektniZadatak.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Osoba")]
    public partial class Osoba
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Osoba()
        {
            Adresa = new HashSet<Adresa>();
            EmailAdresa= new HashSet<EmailAdresa>();
            FiksniTelefon = new HashSet<FiksniTelefon>();
            MobilniTelefon = new HashSet<MobilniTelefon>();
        }

        public int OsobaId { get; set; }

        [Required(ErrorMessage = "Unesite ime")]
        [MinLength(2, ErrorMessage = "Minimum 2 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
        [RegularExpression("^^[a-zA-ZšđčćžŠĐČĆŽ]+$", ErrorMessage = "Ime nije ispravno uneto")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Unesite prezime")]
        [MinLength(2, ErrorMessage = "Minimum 2 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
        [RegularExpression("^[a-zA-ZšđčćžŠĐČĆŽ]+$", ErrorMessage = "Prezime nije ispravno uneto")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unesite ime roditelja")]
        [MinLength(2, ErrorMessage = "Minimum 2 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
        [RegularExpression("^[a-zA-ZšđčćžŠĐČĆŽ]+$", ErrorMessage = "Ime roditelja nije ispravno uneto")]
        public string ImeRoditelja { get; set; }


        [Required(ErrorMessage = "Unesite JMBG")]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "JMBG nije ispravno unet")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Unesite broj lične karte")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Broj lične karte nije ispravno unet")]
        public string BrojLicneKarte { get; set; }

        [Required(ErrorMessage = "Polje mora biti popunjeno u formatu GGGG-MM-DD")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }


        public int? OpstinaRodjenjaId { get; set; }

        public int? PolId { get; set; }

        public byte[] Fotografija { get; set; }

        [Column(TypeName = "text")]
        public string Beleska { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adresa> Adresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailAdresa> EmailAdresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FiksniTelefon> FiksniTelefon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MobilniTelefon> MobilniTelefon { get; set; }

        public virtual Opstina Opstina { get; set; }

        public virtual Pol Pol { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektniZadatak.Models
{
    public class OsobaViewModel
    {
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
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }

        public int OpstinaRodjenjaId { get; set; }

        public int PolId { get; set; }


        public byte[] Fotografija { get; set; }

        public string Beleska { get; set; }

        //Email adresa


        [DataType(DataType.EmailAddress, ErrorMessage = "Email nije ispravno unet")]
        public string NazivEmailAdrese { get; set; }

        public int TipEmailAdreseId { get; set; }

        //Fiksni telefon

        public int LokalFiksniId { get; set; }

        public int TipFiksniId { get; set; }

    
        [RegularExpression("^[0-9]{6,7}$", ErrorMessage = "Broj nije ispravno unet")]
        public string BrojFiksnog { get; set; }


        //Mobilni telefon

        public int LokalMobilniId { get; set; }

        public int TipMobilniId { get; set; }


  
        [RegularExpression("^[0-9]{6,7}$", ErrorMessage = "Broj nije ispravno unet")]
        public string BrojMobilnog { get; set; }

        //Adresa

        [MinLength(3, ErrorMessage = "Minimum 3 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
        [RegularExpression("^[0-9a-zA-ZšđčćžŠĐČĆŽ ]+$", ErrorMessage = "Nije ispravno uneta adresa")]
        public string NazivAdrese { get; set; }

        public int TipAdreseId { get; set; }

        public int GradId { get; set; }


    }
}
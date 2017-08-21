using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjektniZadatak.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Unesite email adresu")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Email adresa nije pravilnog formata")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite lozinku")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }
    }

      public class RegistracijaModel
      {
          [Required]
          [Display(Name = "Pravo pristupa")]
          public string PravoPristupa{ get; set; }


          [Required(ErrorMessage = "Unesite email adresu")]
          [EmailAddress(ErrorMessage = "Email adresa nije pravilnog formata")]
          [Display(Name = "Email")]
          public string Email { get; set; }

          [Required(ErrorMessage = "Unesite ime")]
          [Display(Name = "Ime")]
          [MinLength(2, ErrorMessage = "Minimum 2 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
          [RegularExpression("^^[a-zA-ZšđčćžŠĐČĆŽ]+$", ErrorMessage = "Ime nije ispravno uneto")]
          public string Ime { get; set; }

          [Required(ErrorMessage = "Unesite prezime")]
          [Display(Name = "Prezime")]
          [MinLength(2, ErrorMessage = "Minimum 2 karaktera"), MaxLength(30, ErrorMessage = "Maksimum 30 karaktera")]
          [RegularExpression("^[a-zA-ZšđčćžŠĐČĆŽ]+$", ErrorMessage = "Prezime nije ispravno uneto")]
          public string Prezime { get; set; }


          [Display(Name = "Fotografija")]
          public byte[] Fotografija { get; set; }

          [Required(ErrorMessage = "Izaberite pol")]
          [Display(Name = "Pol")]
          public string Pol { get; set; }

          [Required(ErrorMessage ="Unesite lozinku")]
          [StringLength(100, ErrorMessage = "Lozinka mora da sadrži najmanje{0} i najvise {2} karaktera.", MinimumLength = 6)]
          [DataType(DataType.Password, ErrorMessage ="Lozinka mora da sadrži bar jedno veliko slovo, broj i specijalni karakter")]
          [Display(Name = "Lozinka")]
          public string Lozinka { get; set; }

          [DataType(DataType.Password)]
          [Display(Name = "Potvrdi lozinku")]
          [Compare("Lozinka", ErrorMessage = "Lozinke se ne poklapaju")]
          public string PotvrdiLozinku { get; set; }
      }
      

    public class ResetPasswordViewModel
      {
          [Required]
          [EmailAddress(ErrorMessage = "Email adresa nije pravilnog formata")]  
          [Display(Name = "Email")]
          public string Email { get; set; }

          [Required]
          [StringLength(100, ErrorMessage = "Lozinka mora sadržati najmanje {0} karaktera i ne sme biti duža od {2} karaktera.", MinimumLength = 6)]
          [DataType(DataType.Password, ErrorMessage = "Lozinka mora da sadrži bar jedno veliko slovo, broj i specijalni karakter")]
          [Display(Name = "Lozinka")]
          public string Password { get; set; }

          [DataType(DataType.Password)]
          [Display(Name = "Potvrdi lozinku")]
          [Compare("Password", ErrorMessage = "Lozinke se ne poklapaju.")]
          public string ConfirmPassword { get; set; }

          public string Code { get; set; }
      }
      


    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

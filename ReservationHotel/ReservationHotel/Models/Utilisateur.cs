using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationHotel.Models
{
    public class Utilisateur
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }





        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Display(Name = "Nom")]
        public string? Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Display(Name = "Prénom")]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "L'adresse email est obligatoire.")]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [EmailAddress]
        [Display(Name = "Adresse email")]
        public string? Email { get; set; }



        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Display(Name = "Mot de passe")]
        public string? MotDePasse { get; set; }




        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}

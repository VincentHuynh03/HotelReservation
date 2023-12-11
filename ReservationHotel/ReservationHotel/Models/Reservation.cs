using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservationHotel.Models
{
    public class Reservation
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "La date de début de la chambre est obligatoire.")]
        [Column(TypeName = "Date")]
        [Display(Name = "Date de début")]
        public DateTime? DateDebut { get; set; }


        [Required(ErrorMessage = "La date de fin de la chambre est obligatoire.")]
        [Column(TypeName = "Date")]
        [Compare(nameof(DateDebut), ErrorMessage = "La date de fin ne peut pas être avant la date de début.")]
        [Display(Name = "Date de fin")]
        public DateTime? DateFin { get; set; }

        [Required]
        public bool? Annuler { get; set; } = false;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Utilisateur? Utilisateur { get; set; }

        public int ChambreId { get; set; }
        [ForeignKey("ChambreId")]
        public virtual Chambre? Chambre { get; set; }





    }
}

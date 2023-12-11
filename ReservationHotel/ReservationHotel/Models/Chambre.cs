﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReservationHotel.Models
{
    public class Chambre
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChambreId { get; set; }

        [Required(ErrorMessage = "Le numéro de chambre est obligatoire.")]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Display(Name = "Numéro de chambre")]
        public string? NumeroChambre { get; set; }




        [Required(ErrorMessage = "Une description de la chambre est obligatoire.")]
        [AllowHtml]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Description de la chambre")]
        [MaxLength(6000, ErrorMessage = "La description ne doit pas dépasser 6000 caractères.")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Le type de la chambre est obligatoire.")]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Display(Name = "Type de la chambre")]
        public string? TypeChambre { get; set; }

        [Required(ErrorMessage = "La quantité de personnes est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à zéro.")]
        [Display(Name = "Quantité de personnes")]
        public int? QuantitePersonnes { get; set; }

        [Required(ErrorMessage = "Le prix de la chambre est obligatoire.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro.")]
        [Display(Name = "Prix de la chambre")]
        public decimal? Prix { get; set; }



        [Required(ErrorMessage = "La maximum quantité de personne est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à zéro.")]
        [Display(Name = "Maximum quantité de personnes")]
        public int? MaxPersonne { get; set; }


        [Required(ErrorMessage = "Une vue de la chambre est obligatoire.")]
        [AllowHtml]
        [DataType(DataType.Text)]
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Vue de la chambre")]
        [MaxLength(6000, ErrorMessage = "La vue ne doit pas dépasser 6000 caractères.")]
        public string? Vue { get; set; }


        [Required(ErrorMessage = "La  quantité de lit est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à zéro.")]
        [Display(Name = "Nombre de lit")]
        public int? Lit { get; set; }
        [Required(ErrorMessage = "La date de début de la chambre est obligatoire.")]
        [DataType(DataType.Date)]
        public DateTime? CheckIn { get; set; }

        [Required(ErrorMessage = "La date de fin de la chambre est obligatoire.")]
        [DataType(DataType.Date)]
        public DateTime? CheckOut { get; set; }



        [DataType(DataType.ImageUrl)]
        [Display(Name = "Photo de la chambre")]
        public string? Photo { get; set; }





        //public virtual ICollection<Reservation> Reservations { get; set; }

    }
}

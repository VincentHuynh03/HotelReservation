using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ReservationHotel.Models
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }




        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }




        public void SeedData()
        {
            var sampleChambres = TestData.GenerateChambres();
            var sampleUtilisateurs = TestData.GenerateUtilisateurs();

            var existingChambreNumeros = new HashSet<string>(Chambres.Select(c => c.NumeroChambre));
            foreach (var chambre in sampleChambres)
            {
                if (!existingChambreNumeros.Contains(chambre.NumeroChambre))
                {
                    Chambres.Add(chambre);
                }
            }

            var existingUtilisateurEmails = new HashSet<string>(Utilisateurs.Select(u => u.Email));
            foreach (var utilisateur in sampleUtilisateurs)
            {
                if (!existingUtilisateurEmails.Contains(utilisateur.Email))
                {
                    Utilisateurs.Add(utilisateur);
                }
            }

            var sampleReservations = TestData.GenerateReservations(Chambres.ToList(), Utilisateurs.ToList());

            var existingReservationIds = new HashSet<int>(Reservations.Select(r => r.ReservationId));
            foreach (var reservation in sampleReservations)
            {
                if (!existingReservationIds.Contains(reservation.ReservationId))
                {
                    Reservations.Add(reservation);
                }
            }

            SaveChanges();
        }

    }

}


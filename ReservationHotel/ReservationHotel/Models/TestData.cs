namespace ReservationHotel.Models
{
    public class TestData
    {
        public static List<Chambre> GenerateChambres()
        {
            var chambres = new List<Chambre>
            {
                new Chambre
                {
                    NumeroChambre = "101",
                    Description = "Description de la chambre 101",
                    TypeChambre = "Simple",
                    QuantitePersonnes = 1,
                    Prix = 100.00m,
                    MaxPersonne = 1,
                    Vue = "Vue sur la mer",
                    Lit = 1,
                    Photo = "~/images/chambre1.jpg"
                },
                new Chambre
                {
                    NumeroChambre = "102",
                    Description = "Description de la chambre 102",
                    TypeChambre = "Simple",
                    QuantitePersonnes = 1,
                    Prix = 110.00m,
                    MaxPersonne = 1,
                    Vue = "Vue sur la mer",
                    Lit = 1,
                    Photo = "~/images/chambre2.jpg"
                },
             new Chambre
                {
                    NumeroChambre = "103",
                    Description = "Description de la chambre 103",
                    TypeChambre = "Simple",
                    QuantitePersonnes = 1,
                    Prix = 120.00m,
                    MaxPersonne = 1,
                    Vue = "Vue sur la mer",
                    Lit = 1,
                   Photo = "~/images/chambre3.jpg"
                },
           new Chambre
                {
                    NumeroChambre = "104",
                    Description = "Description de la chambre 104",
                    TypeChambre = "Simple",
                    QuantitePersonnes = 1,
                    Prix = 130.00m,
                    MaxPersonne = 1,
                    Vue = "Vue sur la mer",
                    Lit = 1,
                    Photo = "~/images/chambre4.jpg"
                },
           new Chambre
                {
                    NumeroChambre = "105",
                    Description = "Description de la chambre 105",
                    TypeChambre = "Simple",
                    QuantitePersonnes = 1,
                    Prix = 140.00m,
                    MaxPersonne = 1,
                    Vue = "Vue sur la mer",
                    Lit = 1,
                    Photo = "~/images/chambre5.jpg"
                },




                //Ajout plus de chambre ici si on veut
            };

            return chambres;
        }

        public static List<Reservation> GenerateReservations(List<Chambre> chambres, List<Utilisateur> utilisateurs)
        {
            var reservations = new List<Reservation>();

            if (utilisateurs.Count > 0 && chambres.Count > 0)
            {
                reservations.Add(new Reservation
                {
                    DateDebut = DateTime.Now.AddDays(5),
                    DateFin = DateTime.Now.AddDays(10),
                    Annuler = false,
                    Utilisateur = utilisateurs[0],
                    Chambre = chambres[0]
                });

                if (utilisateurs.Count > 0 && chambres.Count > 0)
                {
                    reservations.Add(new Reservation
                    {
                        DateDebut = DateTime.Now.AddDays(2),
                        DateFin = DateTime.Now.AddDays(7),
                        Annuler = false,
                        Utilisateur = utilisateurs[0],
                        Chambre = chambres[0]
                    });
                }

                if (utilisateurs.Count > 0 && chambres.Count > 0)
                {
                    reservations.Add(new Reservation
                    {
                        DateDebut = DateTime.Now.AddDays(8),
                        DateFin = DateTime.Now.AddDays(12),
                        Annuler = true,
                        Utilisateur = utilisateurs[0],
                        Chambre = chambres[0]
                    });
                }

                // ajout if on veut plus
            }

            return reservations;
        }

        public static List<Utilisateur> GenerateUtilisateurs()
        {
            var utilisateurs = new List<Utilisateur>
            {
                new Utilisateur
                {
                    Nom = "Doe",
                    Prenom = "John",
                    Email = "john.doe@example.com",
                    MotDePasse = "password123"
                },

                new Utilisateur
                {
                    Nom = "Doe",
                    Prenom = "Joan",
                    Email = "joan.doe@example.com",
                    MotDePasse = "password123"
                },

                  //Ajout plus de utilisateurs ici si on veut


            };

            return utilisateurs;
        }
    }

}


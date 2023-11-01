function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

// Initialisation des dates d'arrivée et de départ
var today = new Date();
var nextWeek = new Date(today.getTime() + 7 * 24 * 60 * 60 * 1000);  // Ajoute 7 jours à la date d'aujourd'hui

var formattedToday = formatDate(today);

document.getElementById('checkin-date').value = formattedToday;
document.getElementById('checkout-date').value = formatDate(nextWeek);

// Définition de la date minimale pour les champs de date d'arrivée et de départ
document.getElementById('checkin-date').min = formattedToday;
document.getElementById('checkout-date').min = formattedToday;

//Restriction choix utilisateur dans les champs
document.getElementById('checkin-date').addEventListener('input', function() {
    var checkinDate = new Date(document.getElementById('checkin-date').value);
    var checkoutDate = new Date(document.getElementById('checkout-date').value);

    // Si la date d'arrivée est après la date de départ, mettez à jour la date de départ
    if (checkinDate >= checkoutDate) {
        var newCheckoutDate = new Date(checkinDate.getTime() + 24 * 60 * 60 * 1000);  // Ajoute 1 jour à la date d'arrivée
        document.getElementById('checkout-date').value = formatDate(newCheckoutDate);
    }

    document.getElementById('checkout-date').min = document.getElementById('checkin-date').value;
});

document.getElementById('checkout-date').addEventListener('input', function() {
    document.getElementById('checkin-date').max = document.getElementById('checkout-date').value;
});

// Appel de la fonction updateEstimations au chargement de la page
updateEstimations();

document.getElementById('reservation-form').addEventListener('input', updateEstimations);

document.getElementById('reservation-form').addEventListener('submit', function(e) {
    e.preventDefault();  // Empêche le formulaire d'être soumis normalement

    // ... votre code pour gérer la soumission du formulaire ici ...
});

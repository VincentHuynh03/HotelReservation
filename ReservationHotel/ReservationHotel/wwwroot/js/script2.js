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

function updateEstimations() {
    var checkinDate = new Date(document.getElementById('checkin-date').value);
    var checkoutDate = new Date(document.getElementById('checkout-date').value);
    var pricePerNight = 150;  // Prix par nuit

    // Calcul du nombre de nuits
    var numberOfNights = (checkoutDate - checkinDate) / (1000 * 60 * 60 * 24);

    // Assurez-vous que numberOfNights n'est pas négatif
    numberOfNights = Math.max(0, numberOfNights);

    // Affichage du nombre de nuits
    document.getElementById('number-of-nights').textContent = numberOfNights;

    // Calcul de l'estimation du prix
    var estimationPrice = numberOfNights * pricePerNight;
    document.getElementById('estimation-price').textContent = estimationPrice + '$';

    // Calcul de l'estimation des taxes
    var estimationTaxes = estimationPrice * 0.1;  // Supposant que le taux de taxe est de 10%
    document.getElementById('estimation-taxes').textContent = estimationTaxes + '$';

    // Calcul du total
    var totalPrice = estimationPrice + estimationTaxes;
    document.getElementById('total-price').textContent = totalPrice + '$';
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

// Définition de la date maximale pour les champs de date de départ
document.getElementById('checkin-date').max = formatDate(nextWeek);
document.getElementById('checkout-date').max = formatDate(nextWeek);

//Restriction choix utilisateur dans les champs
document.getElementById('checkin-date').addEventListener('input', function () {
    var checkinDate = new Date(document.getElementById('checkin-date').value);
    var checkoutDate = new Date(document.getElementById('checkout-date').value);

    // Si la date d'arrivée est après la date de départ, mettez à jour la date de départ
    if (checkinDate >= checkoutDate) {
        var newCheckoutDate = new Date(checkinDate.getTime() + 24 * 60 * 60 * 1000);  // Ajoute 1 jour à la date d'arrivée
        document.getElementById('checkout-date').value = formatDate(newCheckoutDate);
    }

    document.getElementById('checkout-date').min = document.getElementById('checkin-date').value;
});

document.getElementById('checkout-date').addEventListener('input', function () {
    document.getElementById('checkin-date').max = document.getElementById('checkout-date').value;
});

// Appel de la fonction updateEstimations au chargement de la page
updateEstimations();

document.getElementById('reservation-form').addEventListener('input', updateEstimations);

document.getElementById('reservation-form').addEventListener('submit', function (e) {
    e.preventDefault();  // Empêche le formulaire d'être soumis normalement

    // ... votre code pour gérer la soumission du formulaire ici ...
});

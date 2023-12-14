//Create new instance HubConnectionBuilder
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/Home/ServiceClientèle/askHub")
    .configureLogging(signalR.LogLevel.Information) // Configuration du niveau de 
    .build(); // Create new HubConnection object

// Define a function de reception des messages du hub
connection.on("ReceiveMessage", (message) => {
    // prepend le message recu a un element dans le DOM avec l'id 'signalr-message-panel'
    $('#signalr-message-panel').prepend($('<div />').text(message));
});

// Gestionnaire d'evenement pour le clic sur le bouton id 'btn-broadcast'
$('#btn-broadcast').click(function () {
    // receive message from diffusion avec id 'broadcast'
    var message = $('#broadcast').val();

    // Invocation de la methode cote serveur "BroadcastMessage" avec le message specifie
    connection.invoke("BroadcastMessage", message).catch(err => console.error(err.toString()));
});

// Gestionnaire d'événement pour le clic sur le bouton avec l'id 'btn-others-message'
$('#btn-others-message').click(function () {
 // Récupération du message à envoyer aux autres clients depuis un élément avec l'id 'others - message'
    var message = $('#others-message').val();

    // Invocation de la méthode côté serveur "SendToOthers" avec le message spécifié
    connection.invoke("SendToOthers", message).catch(err => console.error(err.toString()));
});

// Gestionnaire d'événement pour le clic sur le bouton avec l'id 'btn-self-message'
$('#btn-self-message').click(function () {
    // Récupération du message à envoyer à soi-même depuis un élément avec l'id 'selfmessage'
    var message = $('#self-message').val();
    // Invocation de la méthode côté serveur "SendToCaller" avec le message spécifié
    connection.invoke("SendToCaller", message).catch(err =>
        console.error(err.toString()));
});

// Attache une fonction de rappel à l'événement de clic du bouton avec l'ID 'btn-individualmessage'
$('#btn-individual-message').click(function () {
    // Récupère la valeur du champ de saisie de message avec l'ID 'individual-message'
    var message = $('#individual-message').val();
    // Récupère la valeur du champ de saisie d'ID de connexion avec l'ID 'connection-for-message'
    var connectionId = $('#connection-for-message').val();
    // Utilise SignalR pour invoquer la méthode côté serveur 'SendToIndividual' avec les valeurs récupérées
    connection.invoke("SendToIndividual", connectionId, message)
        // Gère les erreurs éventuelles lors de l'invocation de la méthode
        .catch(err => console.error(err.toString()));
});

// Associe une fonction de rappel au clic du bouton avec l'ID 'btn-group-message'
$('#btn-group-message').click(function () {
    var message = $('#group-message').val();
    var group = $('#group-for-message').val();
    // Invoque la méthode côté serveur 'SendToGroup' avec le groupe et le message spécifiés
    connection.invoke("SendToGroup", group, message).catch(err =>
        console.error(err.toString()));
});
// Associe une fonction de rappel au clic du bouton avec l'ID 'btn-group-add'
$('#btn-group-add').click(function () {
    var group = $('#group-to-add').val();
    // Invoque la méthode côté serveur 'AddUserToGroup' avec le groupe spécifié
    connection.invoke("AddUserToGroup", group).catch(err => console.error(err.toString()));
});
// Associe une fonction de rappel au clic du bouton avec l'ID 'btn-group-remove'
$('#btn-group-remove').click(function () {
    var group = $('#group-to-remove').val();
    // Invoque la méthode côté serveur 'RemoveUserFromGroup' avec le groupe spécifié
    connection.invoke("RemoveUserFromGroup", group).catch(err =>
        console.error(err.toString()));
});

// Fonction asynchrone pour démarrer la connexion
async function start() {
    try {
        // Tentative de démarrage de la connexion
        await connection.start();
        console.log('connected'); // Affichage dans la console si la connexion est établie
    } catch (err) {
        console.log(err); // Affichage dans la console en cas d'erreur de connexion
        setTimeout(() => start(), 5000); // Nouvelle tentative de connexion après 5 secondes en cas d'échec
    }
}
// Gestionnaire d'événement pour la fermeture de la connexion
connection.onclose(async () => {
    await start(); // Redémarrage de la connexion en cas de fermeture
});
// Démarrage initial de la connexion
start();
//Create new instance HubConnectionBuilder
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/askHub")
    .configureLogging(signalR.LogLevel.Information)
    .build(); // Create new HubConnection object

// Define a function de reception des messages du hub
connection.on("ReceiveMessage", (message) => {
    // prepend le message recu a un element dans le DOM avec l'id 'signalr-message-panel'
    $('signalr-message-panel').prepend($('<div />').text(message));
});

// Gestionnaire d'evenement pour le clic sur le bouton id 'btn-broadcast'
$('#btn-broadcast').click(function () {
    // receive message from diffusion avec id 'broadcast'
    var message = $('#broadcast').val();

    // Invocation de la methode cote serveur "BroadcastMessage" avec le message specifie
    connection.invoke("BroadcastMessage", message).catch(err =>
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
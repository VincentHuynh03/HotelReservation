using Microsoft.AspNetCore.SignalR;

namespace ReservationHotel.Hubs
{
    public class AskHub : Hub<ILearningHubClient>
    {
        // Diffuse un message a tous les clients connectes
        public async Task BroadcastMessage(string message)
        {
            // Appeller la methode ReceiveMessage de tous les client
            // connectes avec le message specifie
            await Clients.All.ReceiveMessage(message);
        }

        // Méthode qui envoie un message à tous les autres clients, excluant l'émetteur
        public async Task SendToOthers(string message)
        {
            // Appelle la méthode ReceiveMessage de tous les autres clients avec le message spécifié
            await Clients.Others.ReceiveMessage(message);
        }

        // Méthode pour envoyer un message au client appelant uniquement
        public async Task SendToCaller(string message)
        {
            // Appelle la méthode ReceiveMessage du client appelant avec le message modifié
            await Clients.Caller.ReceiveMessage(message);
        }

        // Appelle lorsqu'un client se connecte au hub
        public override async Task OnConnectedAsync()
        {
            //Appelle la methode de la classe de base pour effecture les taches
            // liees a la connexion
            await base.OnConnectedAsync();
        }

        // Appelle lorsqu'un client se deconnecte du hub
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            //Appelle la methode de la classe de base pour effecture les taches
            // liees a la deconnexion
            await base.OnDisconnectedAsync(exception);
        }
    }
}

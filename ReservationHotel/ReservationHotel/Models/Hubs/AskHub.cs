using Microsoft.AspNetCore.SignalR;

namespace ReservationHotel.Models.Hubs
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

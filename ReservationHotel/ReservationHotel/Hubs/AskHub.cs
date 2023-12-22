using Microsoft.AspNetCore.SignalR;
using ReservationHotel.Models;

namespace ReservationHotel.Hubs
{
    public class AskHub : Hub<ILearningHubClient>
    {

        //
        //public async Task SendMessage(Message message) =>
        //    await Clients.All.SendAsync("receiveMessage", message);

        // Diffuse un message a tous les clients connectes
        public async Task BroadcastMessage(string user,string message)
        {
            // Appeller la methode ReceiveMessage de tous les client
            // connectes avec le message specifie
            await Clients.All.ReceiveMessage(GetMessageToSend(user,message));
        }

        public async Task BroadcastConnexion(string user)
        {
            // Appeller la methode ReceiveMessage de tous les client
            // connectes avec le message specifie
            await Clients.Others.ReceiveMessage(GetConnexionToSend(user));
        }

        // Méthode qui envoie un message à tous les autres clients, excluant l'émetteur
        //public async Task SendToOthers(string user, string message)
        //{
        //    // Appelle la méthode ReceiveMessage de tous les autres clients avec le message spécifié
        //    await Clients.Others.ReceiveMessage(GetMessageToSend(message));
        //}

        //// Méthode pour envoyer un message au client appelant uniquement
        //public async Task SendToCaller(string user, string message)
        //{
        //    // Appelle la méthode ReceiveMessage du client appelant avec le message modifié
        //    await Clients.Caller.ReceiveMessage(GetMessageToSend(message));
        //}

        // Méthode pour envoyer un message à un client individuel spécifié par son ID deconnexion
        public async Task SendToIndividual(string connectionId, string message)
        {
            // Appelle la méthode ReceiveMessage du client spécifié avec le message modifié
            await Clients.Client(connectionId).ReceiveMessage(GetMessageToSendWU(message));
        }

        // Méthode privée pour obtenir le message à envoyer, incluant l'ID de connexion du client
        private string GetMessageToSendWU(string originalMessage)
        {
            return $"Admin : {originalMessage}";
            //return $"{user} {Context.ConnectionId}: {originalMessage}";

        }

        private string GetMessageToSend(string user,string originalMessage)
        {
            return $"{user} : {originalMessage}";
            //return $"{user} {Context.ConnectionId}: {originalMessage}";

        }

        private string GetConnexionToSend(string user)
        {
            return $"{user} : {Context.ConnectionId}";
            //return $"{user} {Context.ConnectionId}: {originalMessage}";
        }

        // Méthode pour envoyer un message à tous les clients d'un groupe spécifique
        public async Task SendToGroup(string groupName, string message,string user)
        {
            await Clients.Group(groupName).ReceiveGroupMessage(GetMessageToSend(user,message));
        }

        public async Task SendToGroupAdmin(string groupName, string message)
        {
            await Clients.Group(groupName).ReceiveGroupMessage(GetMessageToSendWU(message));
        }
        //// Méthode pour ajouter un utilisateur à un groupe spécifique
        public async Task AddUserToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Caller.ReceiveMessage($"L'utilisateur actuel a été ajouté au groupe {groupName}");
        }
        //// Méthode pour retirer un utilisateur d'un groupe spécifique
        //public async Task RemoveUserFromGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Caller.ReceiveMessage($"L'utilisateur actuel a été retiré du groupe { groupName}");
        //    await Clients.Others.ReceiveMessage($"L'utilisateur {Context.ConnectionId} a été retiré du groupe { groupName}");
        //}

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

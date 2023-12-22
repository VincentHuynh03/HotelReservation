using Microsoft.AspNetCore.SignalR.Client;
//Importation du module SignalR.Client

// Affiche un message demandant à l'utilisateur de spécifier l'URL du hub SignalR
Console.WriteLine("Veuillez spécifier l'URL du SignalR Hub");

// Lit l'URL saisi par l'utilisateur depuis la console
var url = Console.ReadLine();

// Crée une connexion au hub SignalR en utilisant l'URL spécifiée
var hubConnection = new HubConnectionBuilder()
 .WithUrl(url)
 .Build();

// Définit une fonction de rappel pour le message "ReceiveMessage" du hub
hubConnection.On<string>("ReceiveMessage", message => Console.WriteLine($"Message du hub SignalR : {message}"));

// Définit une fonction de rappel pour le message "ReceiveGroupMessage" du hub
hubConnection.On<string>("ReceiveGroupMessage", message => Console.WriteLine($"Message du groupe : {message}"));

try
{
    // Tente de démarrer la connexion au hub SignalR de manière asynchrone
    await hubConnection.StartAsync();
    var running = true;
    // Boucle principale
    while (running)
    {
        var message = string.Empty;
        var groupName = string.Empty;

        // Demande à l'utilisateur de spécifier une action
        Console.WriteLine("Veuillez préciser l'action :");
        Console.WriteLine("0 - diffuser à tous");
        Console.WriteLine("1 - envoyer aux autres");
        Console.WriteLine("2 - envoyer à soi-même");
        Console.WriteLine("3 - envoyer à un individu");
        Console.WriteLine("4 - envoyer à un groupe");
        Console.WriteLine("5 - ajouter un utilisateur à un groupe");
        Console.WriteLine("6 - supprimer un utilisateur d'un groupe");
        Console.WriteLine("exit - Quitter le programme");

        // Lit l'action saisie par l'utilisateur depuis la console
        var action = Console.ReadLine();

        // Si l'action n'est pas liée à l'ajout ou à la suppression d'un utilisateur d'un groupe, demande le message
        if (action != "5" && action != "6")
        {
            Console.WriteLine("Veuillez spécifier le message :");
            message = Console.ReadLine();
        }
        // Si l'action est liée à l'envoi à un groupe, à l'ajout ou à la suppression d'un utilisateur d'un groupe, demande le nom du groupe
        if (action == "4" || action == "5" || action == "6")
        {
            Console.WriteLine("Veuillez spécifier le nom du groupe :");
            groupName = Console.ReadLine();
        }

        // Demande à l'utilisateur de spécifier le message
        Console.WriteLine("Veuillez préciser le message :");
        message = Console.ReadLine();
        
        // Gestion des différentes actions
        switch (action)
        {
            case "0":
                // Envoie un message au hub SignalR avec le nom "BroadcastMessage"
                await hubConnection.SendAsync("BroadcastMessage", message);
                break;
            case "1":
                // Envoie un message au hub SignalR avec le nom "SendToOthers"
                await hubConnection.SendAsync("SendToOthers", message);
                break;
            case "2":
                // Envoie un message au hub SignalR avec le nom "SendToCaller"
                await hubConnection.SendAsync("SendToCaller", message);
                break;
            case "3":
                // Demande à l'utilisateur de spécifier l'ID de connexion de l'individu
                Console.WriteLine("Veuillez préciser l'ID de connexion :");
                var connectionId = Console.ReadLine();
                // Envoie un message au hub SignalR avec le nom "SendToIndividual"
                await hubConnection.SendAsync("SendToIndividual", connectionId, message);
                break;
            case "4":
                // Envoie le message à un groupe spécifié
                hubConnection.SendAsync("SendToGroupAdmin", groupName, message).Wait();
                break;
            case "5":
                // Ajoute l'utilisateur à un groupe spécifié
                hubConnection.SendAsync("AddUserToGroup", groupName).Wait();
                break;
            case "6":
                // Supprime l'utilisateur d'un groupe spécifié
                hubConnection.SendAsync("RemoveUserFromGroup", groupName).Wait();
                break;
            case "exit":
                // Modifie la variable pour sortir de la boucle et terminer le programme
                running = false;
                break;
            default:
                Console.WriteLine("Action spécifiée non valide");
                break;
        }
    }
}
catch (Exception ex)
{
    // Gère les exceptions en affichant le message d'erreur
    Console.WriteLine(ex.Message);
    Console.WriteLine("Appuyez sur une touche pour quitter...");
    Console.ReadKey();
    return;
}
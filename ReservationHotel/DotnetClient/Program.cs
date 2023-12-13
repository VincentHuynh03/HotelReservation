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
try
{
    // Tente de démarrer la connexion au hub SignalR de manière asynchrone
    await hubConnection.StartAsync();
    var running = true;
    // Boucle principale
    while (running)
    {
        var message = string.Empty;
        // Demande à l'utilisateur de spécifier une action
        Console.WriteLine("Veuillez préciser l'action :");
        Console.WriteLine("0 - diffuser à tous");
        Console.WriteLine("1 - envoyer aux autres");
        Console.WriteLine("2 - envoyer à soi-même");
        Console.WriteLine("exit - Quitter le programme");

        // Lit l'action saisie par l'utilisateur depuis la console
        var action = Console.ReadLine();
        
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
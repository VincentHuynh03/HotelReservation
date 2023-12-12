using Microsoft.AspNetCore.SignalR.Client;

// Demande à l'utilisateur de spécifier l'URL du SignalR Hub
Console.WriteLine("Veuillez spécifier l'URL du SignalR Hub");

// Lit l'URL saisi par l'utilisateur depuis la console
var url = Console.ReadLine();

// Crée une instance de HubConnection en utilisant le HubConnectionBuilder avec l'URL spécifiée
var hubConnection = new HubConnectionBuilder()
 .WithUrl(url)
 .Build();

// Définit une fonction de rappel pour le message "ReceiveMessage" du hub
hubConnection.On<string>("ReceiveMessage", message => Console.WriteLine($"SignalR Hub Message: {message}"));
try
{
    // Démarre la connexion au hub SignalR de manière asynchrone
    await hubConnection.StartAsync();

    // Initialise une variable booléenne pour contrôler la boucle while
    var running = true;

    // Boucle principale
    while (running)
    {
        // Demande à l'utilisateur de spécifier une action
        Console.WriteLine("Veuillez préciser l'action:");
        Console.WriteLine("0 - diffusé à tous");
        Console.WriteLine("exit - Quitter le programme");
        var action = Console.ReadLine();

        // Demande à l'utilisateur de préciser le message
        Console.WriteLine("Merci de préciser le message:");
        var message = Console.ReadLine();

        // Gestion des différentes actions
        switch (action)
        {
            case "0":
                // Envoie un message au hub SignalR avec le nom "BroadcastMessage" et le message spécifié        await hubConnection.SendAsync("BroadcastMessage", message);
                await hubConnection.SendAsync("BroadcastMessage", message);
                break;
            case "exit":
                // Modifie la variable pour sortir de la boucle while et terminer le programme
                 running = false;
                break;
            default:
                // Affiche un message en cas d'action non valide
                Console.WriteLine("Invalid action specified");
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
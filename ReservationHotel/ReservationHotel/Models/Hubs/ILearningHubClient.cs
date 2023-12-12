namespace ReservationHotel.Models.Hubs
{
    public interface ILearningHubClient
    {
        Task ReceiveMessage(string message);
    }
}

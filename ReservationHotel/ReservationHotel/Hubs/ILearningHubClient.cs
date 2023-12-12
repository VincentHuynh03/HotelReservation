namespace ReservationHotel.Hubs
{
    public interface ILearningHubClient
    {
        Task ReceiveMessage(string message);
    }
}

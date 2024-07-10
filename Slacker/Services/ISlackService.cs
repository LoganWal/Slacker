namespace Slacker.Services
{
    public interface ISlackService
    {
        Task UpdateSlackStatus(string text, string emoji);
    }
}
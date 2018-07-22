using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace StatusBot.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// Handles the messages.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        Task HandleMessages(Activity activity);
    }
}
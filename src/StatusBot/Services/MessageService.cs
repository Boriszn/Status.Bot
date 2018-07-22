using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusBot.DataSources;

namespace StatusBot.Services
{
    /// <inheritdoc />
    public class MessageService : IMessageService
    {
        private readonly MicrosoftAppCredentials appCredentials;
        private readonly IGitHubRepository gitHubRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="gitHubRepository">The git hub repository.</param>
        public MessageService(
            IConfiguration configuration,
            IGitHubRepository gitHubRepository)
        {
            appCredentials = new MicrosoftAppCredentials(configuration);
            this.gitHubRepository = gitHubRepository;
        }

        /// <inheritdoc />
        public async Task HandleMessages(Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                string message = activity.Text.ToLower();

                switch (message)
                {
                    case var _ when message.Contains("info") is true:
                        message = await gitHubRepository.GetUserInfo();
                        break;
                    case var _ when message.Contains("my issues") is true:
                        message = await gitHubRepository.GetCurrentUserIssuesInfoString();
                        break;
                    case var _ when message.Contains("issues") is true:
                        message = await gitHubRepository.GetUserIssuesInfoString();
                        break;
                    default:
                         message = $"I don't know about that ....";
                        break;
                }

                await ReplyMessage(activity, message);
            }
            else
            {
                await HandleSystemMessage(activity);
            }
        }

        /// <summary>
        /// Replies the message.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="message">The message.</param>
        private async Task ReplyMessage(Activity activity, string message)
        {
            var serviceEndpointUri = new Uri(activity.ServiceUrl);
            var connector = new ConnectorClient(serviceEndpointUri, appCredentials);
            var reply = activity.CreateReply(message);

            await connector.Conversations.ReplyToActivityAsync(reply);
        }

        /// <summary>
        /// Handles the system message.
        /// </summary>
        /// <param name="activity">The activity.</param>
        private async Task<Activity> HandleSystemMessage(Activity activity)
        {
            switch (activity.Type)
            {
                case ActivityTypes.DeleteUserData:
                    // Implement user deletion here
                    // If we handle user deletion, return a real message
                    break;

                case ActivityTypes.ConversationUpdate:
                    // Handle conversation state changes, like members being added and removed
                    // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                    // Not available in all channels
                    break;

                case ActivityTypes.ContactRelationUpdate:
                    // Handle add/remove from contact lists
                    // Activity.From + Activity.Action represent what happened
                    break;

                case ActivityTypes.Typing:
                    // Handle knowing that the user is typing
                    break;

                case ActivityTypes.Ping:
                    await ReplyMessage(activity, "Pong");
                    break;
            }

            return null;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace PanvivaBot
{
    public class PanvivaBot : IBot
    {
        private readonly PanvivaBotAccessors _accessors;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanvivaBot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public PanvivaBot(PanvivaBotAccessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<PanvivaBot>();
            _logger.LogTrace("PanvivaBot turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
        }

        /// <summary>
        /// Every conversation turn for our Panviva Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                if (string.Equals(turnContext.Activity.Text.ToUpper(), "HI") ||
                    string.Equals(turnContext.Activity.Text.ToUpper(), "HELLO") ||
                    string.Equals(turnContext.Activity.Text.ToUpper(), "HELP"))
                {
                    await SendGreetingAsync(turnContext);
                }
                else
                {
                    await SearchPanvivaAndSendResultsToUserAsync(turnContext);
                }
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded[0].Id != "default-bot")
                {
                    await SendGreetingAsync(turnContext);
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }

        private static async Task SearchPanvivaAndSendResultsToUserAsync(ITurnContext turnContext)
        {
            var responseMessage = $"Searching for information about '{turnContext.Activity.Text}'\n";
            await turnContext.SendActivityAsync(responseMessage);
            var panvivaResponse = await PanvivaAPI.NaturalLanguageSearchAsync(turnContext.Activity.Text);
            if (panvivaResponse.Count > 0)
            {
                responseMessage = $"I found the following:";
                foreach (var response in panvivaResponse)
                {
                    responseMessage += $"\n- {response.ResponseContent} in category '{response.Category}'";
                }

                await turnContext.SendActivityAsync(responseMessage);
            }
            else
            {
                await turnContext.SendActivityAsync($"I couldn't find anything about '{turnContext.Activity.Text}'");
            }
        }

        private static async Task SendGreetingAsync(ITurnContext turnContext)
        {
            await turnContext.SendActivityAsync($"Hello from the Panviva Bot!\n\nI can help you find information in Panviva's knowledge base.  Type in a topic of interest, and I'll see what I can find.");
        }
    }
}

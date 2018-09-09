using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusBot.Services;

namespace StatusBot.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageService messageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="messageService">The message service.</param>
        public MessagesController(
            IMessageService messageService)
        {
            this.messageService = messageService;
        }

        /// <summary>
        /// Posts the specified activity.
        /// POST api/values
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<OkResult> Post([FromBody]Activity activity)
        {
            this.messageService.HandleMessages(activity);

            return Ok();
        }
    }
}

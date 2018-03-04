using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HtHInAction.Services.Hubs
{
    public class EmailNotificationsHub : Hub
    {
       
    }

    public class EmailNotifications : HostedService
    {
        private readonly IEmailService _emailService;

        public EmailNotifications(IHubContext<EmailNotificationsHub> context, IEmailService emailService )
        {
            Clients = context.Clients;
            _emailService = emailService;
        }

        private IHubClients Clients { get; }

        public async Task UpdateMailRiceived()
        {
            var num = await _emailService.ReceivedEmailAsync();
            await Clients.All.InvokeAsync("emailNotifications", num);
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {           

            while (true)
            {
                await UpdateMailRiceived();

                var task = Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);
                try
                {
                    await task;
                }
                catch (TaskCanceledException)
                {
                    return;
                }
            }
        }
    }
}

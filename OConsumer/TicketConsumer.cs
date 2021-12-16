using MassTransit;
using Shared.Model;

namespace OConsumer
{
    public class TicketConsumer: IConsumer<Ticket>
    {
        private readonly ILogger<TicketConsumer> logger;
        public TicketConsumer(ILogger<TicketConsumer> logger)
        {
            this.logger = logger;   
        }
        public async Task Consume(ConsumeContext<Ticket> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Booked.ToString()); 

            logger.LogInformation("$Nova mensagem recebida: " + 
                $"{ context.Message.Booked} {context.Message.UserName}");       
        }
    }
}

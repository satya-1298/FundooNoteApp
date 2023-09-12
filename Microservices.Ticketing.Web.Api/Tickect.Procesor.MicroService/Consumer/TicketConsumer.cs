using MassTransit;
using Shared.Model;
using System.Threading.Tasks;
namespace Tickect.Procesor.MicroService.Consumer
{
    public class TicketConsumer : IConsumer<TicketModel>
    {

        public async Task Consume(ConsumeContext<TicketModel> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}

using CommonLayer.Model;
using MassTransit;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace TicketService.Service
{
    public class TicketConsumer : IConsumer<CollabCreateModel>
    {

        public async Task Consume(ConsumeContext<CollabCreateModel> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
            string email=data.Email;
            string subject = "Collabration Email";
            string body = "This Email Want to Collaborate with you";
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("satyaadabala369@gmail.com", "vjotjsdzfiswhayj"),
                EnableSsl = true
            };

            SMTP.Send( "satyaadabala369@gmail.com",email, subject, body);
        }
    }

}
